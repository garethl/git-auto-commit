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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GitAutoCommit.Core;
using GitAutoCommit.Forms;

namespace GitAutoCommit
{
	internal static class Program
	{
		private static readonly List<AutoCommitHandler> Handlers = new List<AutoCommitHandler>();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            GACApplication application;
            if (args.Length == 0)
            {
                application = new GACApplication(true);
            }
            else
            {
                int interval;
                string error = null;
                if (args.Length < 2 || !int.TryParse(args[0], out interval) || !AllDirectoriesExist(args.Skip(1), out error))
                {
                    if (string.IsNullOrEmpty(error))
                        error = "Invalid command line arguments";

                    error = error + "\r\n\r\n" +
                            "usage: git-auto-commit <commit-interval-seconds> <directory 1>, <directory 2>, ..., <directory n>";

                    MessageBox.Show(error, "git-auto-commit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                    return;
                }

                var tasks = args.Skip(1).Select(x => new AutoCommitTask(interval, x));
                application = new GACApplication(true, tasks);
            }

            var icon = new NotifyIconController(application);
			icon.Show();

			Application.Run();
		}

		private static bool AllDirectoriesExist(IEnumerable<string> directories, out string error)
		{
			foreach (var directory in directories)
			{
				if (!Directory.Exists(directory))
				{
					error = "Directory {0} doesn't exist";
					return false;
				}

				if (!Directory.Exists(Path.Combine(directory, ".git")))
				{
					error = "Directory {0} is not a git repository";
					return false;
				}
			}

			error = null;
			return true;
		}
	}
}