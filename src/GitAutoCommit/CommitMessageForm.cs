using System;
using System.Windows.Forms;

namespace GitAutoCommit
{
	public partial class CommitMessageForm : Form
	{
		public CommitMessageForm()
		{
			InitializeComponent();
		}

		public bool GetMessage(ref string message, ref bool prependAutoCommit)
		{
			if (ShowDialog() == DialogResult.OK)
			{
				message = commitMessageTextBox.Text;
				prependAutoCommit = prependAutoCommitCheckbox.Checked;

				return true;
			}

			return false;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}