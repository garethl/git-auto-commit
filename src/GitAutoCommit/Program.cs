using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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

			foreach (var folder in args.Skip(1))
			{
				var handler = new AutoCommitHandler(interval, folder);
				Handlers.Add(handler);
			}

			Application.Run(new MainForm());
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