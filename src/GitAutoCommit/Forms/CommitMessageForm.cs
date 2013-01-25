using System.Linq;
using System;
using System.Windows.Forms;

namespace GitAutoCommit.Forms
{
	public partial class CommitMessageForm : BaseForm
	{
		public CommitMessageForm()
		{
			InitializeComponent();

		    commitMessageTextBox.Font = FontHelper.MonospaceFont;
		}

		public bool GetMessage(ref string message, ref bool prependAutoCommit)
		{
			commitMessageTextBox.Text = message;
			prependAutoCommitCheckbox.Checked = prependAutoCommit;

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