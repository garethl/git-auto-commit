using System;
using System.Windows.Forms;
using GitAutoCommit.Properties;

namespace GitAutoCommit
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			notifyIcon.Icon = Resources.icon_16;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}