using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GitAutoCommit.Properties;

namespace GitAutoCommit
{
	internal class NotifyIconController
	{
		private readonly ContextMenuStrip _contextMenu;
		private readonly IEnumerable<AutoCommitHandler> _handlers;
		private readonly NotifyIcon _notifyIcon;

		public NotifyIconController(IEnumerable<AutoCommitHandler> handlers)
		{
			_notifyIcon = new NotifyIcon();
			_contextMenu = new ContextMenuStrip();
			_contextMenu.Items.AddRange(
				new[]
					{
						new ToolStripMenuItem("&Set commit message", null, (s, e) => SetCommitMessage()),
						new ToolStripMenuItem("E&xit", null, (s, e) => Close())
					}
				);

			_notifyIcon.ContextMenuStrip = _contextMenu;
			_notifyIcon.Icon = Resources.icon_16;
			_notifyIcon.Text = "Git auto commit";

			_notifyIcon.DoubleClick += delegate { SetCommitMessage(); };

			_handlers = handlers;
		}

		private void SetCommitMessage()
		{
			using (var form = new CommitMessageForm())
			{
				string message;
				bool prependAutoCommit;

				_handlers.First().GetCommitMessage(out message, out prependAutoCommit);

				if (form.GetMessage(ref message, ref prependAutoCommit))
				{
					foreach (var handler in _handlers)
					{
						handler.SetCommitMessage(message, prependAutoCommit);
					}
				}
			}
		}

		public void Show()
		{
			_notifyIcon.Visible = true;

			SetCommitMessage();
		}

		public void Close()
		{
			_notifyIcon.Visible = false;
			Application.Exit();
		}
	}
}