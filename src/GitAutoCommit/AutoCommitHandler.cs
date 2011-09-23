#region License
/*
Copyright (c) 2011 Gareth Lennox (garethl@dwakn.com)
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice,
    this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice,
    this list of conditions and the following disclaimer in the documentation
    and/or other materials provided with the distribution.
    * Neither the name of Gareth Lennox nor the names of its
    contributors may be used to endorse or promote products derived from this
    software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
*/
#endregion
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;

namespace GitAutoCommit
{
	public class AutoCommitHandler : IDisposable
	{
		private readonly ConcurrentBag<string> _changes = new ConcurrentBag<string>();
		private readonly string _folder;
		private readonly Timer _timer;
		private readonly FileSystemWatcher _watcher;

		public AutoCommitHandler(int intervalSeconds, string folder)
		{
			_folder = folder;

			Console.WriteLine("Watching {0} for changes", _folder);

			_watcher = new FileSystemWatcher(_folder) {IncludeSubdirectories = true};
			_watcher.Changed += watcher_Changed;
			_watcher.Created += watcher_Created;
			_watcher.Renamed += watcher_Renamed;

			_watcher.EnableRaisingEvents = true;

			_timer = new Timer(intervalSeconds*1000);
			_timer.Elapsed += _timer_Elapsed;
			_timer.Start();
		}

		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (_changes.Count == 0)
				return;

			try
			{
				_timer.Stop();

				var changes = new HashSet<string>();

				string result;
				while (_changes.TryTake(out result))
					changes.Add(result);

				if (changes.Count > 0)
				{
					foreach (var file in changes)
					{
						//no file...
						if (!File.Exists(file))
							continue;

						Console.WriteLine("Committing changes to {0}", file);
						RunGit("add \"" + file + "\"");
					}

					RunGit("commit -m \"AUTOCOMMIT\"");
				}
			}
			finally
			{
				_timer.Start();
			}
		}

		private void RunGit(string arguments)
		{
			var start = new ProcessStartInfo("git.exe", arguments)
			            	{
			            		WorkingDirectory = _folder,
			            		CreateNoWindow = true,
			            		UseShellExecute = false,
			            		WindowStyle = ProcessWindowStyle.Hidden,
			            		RedirectStandardError = true
			            	};

			var process = Process.Start(start);

			var error = process.StandardError.ReadToEnd();

			if (!string.IsNullOrWhiteSpace(error))
				Console.WriteLine(error);

			process.WaitForExit();
		}

		private void watcher_Renamed(object sender, RenamedEventArgs e)
		{
			if (e.Name.StartsWith(".git") || e.Name.EndsWith(".tmp"))
				return;

			_changes.Add(e.FullPath);
		}

		private void watcher_Created(object sender, FileSystemEventArgs e)
		{
			if (e.Name.StartsWith(".git") || e.Name.EndsWith(".tmp"))
				return;

			_changes.Add(e.FullPath);
		}

		private void watcher_Changed(object sender, FileSystemEventArgs e)
		{
			if (e.Name.StartsWith(".git") || e.Name.EndsWith(".tmp"))
				return;

			_changes.Add(e.FullPath);
		}

		#region Implementation of IDisposable

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			_watcher.Dispose();
		}

		#endregion
	}
}