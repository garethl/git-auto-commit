using System;
using System.Linq;
using System.Windows.Forms;
using GitAutoCommit.Core;
using GitAutoCommit.Forms;
using GitAutoCommit.Properties;

namespace GitAutoCommit
{
    public class NotifyIconController
    {
        private readonly GACApplication _application;
        private readonly ContextMenuStrip _contextMenu;
        private readonly NotifyIcon _notifyIcon;

        private MainForm _mainForm;

        public NotifyIconController(GACApplication application)
        {
            _application = application;
            _notifyIcon = new NotifyIcon();
            _contextMenu = new ContextMenuStrip();
            _contextMenu.Items.AddRange(
                new[]
                    {
                        new ToolStripMenuItem("&Configuration", null, (s, e) => ShowMainForm()),
                        new ToolStripMenuItem("E&xit", null, (s, e) => Close())
                    }
                );

            _notifyIcon.ContextMenuStrip = _contextMenu;
            _notifyIcon.Icon = Resources.icon_16;
            _notifyIcon.Text = "Git auto commit";

            _notifyIcon.DoubleClick += delegate { ShowMainForm(); };

            if (!application.IsCommandLineDriven && application.Tasks.Count == 0)
            {
                ShowMainForm();
            }
        }

        private void ShowMainForm()
        {
            if (_mainForm == null)
            {
                _mainForm = new MainForm(_application);
                _mainForm.Closed += (sender, args) => _mainForm = null;
            }

            _mainForm.Show();
        }

        public void Show()
        {
            _notifyIcon.Visible = true;
        }

        public void Close()
        {
            _notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}