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