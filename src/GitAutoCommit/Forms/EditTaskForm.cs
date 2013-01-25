using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GitAutoCommit.Core;

namespace GitAutoCommit.Forms
{
    public partial class EditTaskForm : HeadingForm
    {
        public EditTaskForm()
        {
            InitializeComponent();
        }
        
        public DialogResult EditTask(AutoCommitTask item, IWin32Window owner)
        {
            Bind(item);

            var result = ShowDialog(owner);

            if (result == DialogResult.OK)
            {
                UnBind(item);
            }

            return result;
        }

        private void UnBind(AutoCommitTask item)
        {
            item.SetProperties(nameTextBox.Text, folderTextBox.Text, commitMessageTextBox.Text);
        }

        private void Bind(AutoCommitTask item)
        {
            if (string.IsNullOrEmpty(item.Handler.Folder))
                Text = "add task";

            nameTextBox.Text = item.Name;
            folderTextBox.Text = item.Folder;
            commitMessageTextBox.Text = item.CommitMessage;


            if (nameTextBox.Text == "" && folderTextBox.Text != "")
                nameTextBox.Text = Path.GetFileName(folderTextBox.Text);

            if (commitMessageTextBox.Text == "")
                commitMessageTextBox.Text = "Automatic commit";
        }

        private bool FormIsValid()
        {
            if (folderTextBox.Text == "" || !Directory.Exists(folderTextBox.Text))
            {
                MessageBox.Show(this, "Please enter a valid folder", "git auto commit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //check if it is a git repository
            if (!Directory.Exists(Path.Combine(folderTextBox.Text, ".git")))
            {
                MessageBox.Show(this, "The selected folder doesn't seem to be a git repository", "git auto commit", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (nameTextBox.Text == "")
                nameTextBox.Text = Path.GetFileName(folderTextBox.Text);

            if (commitMessageTextBox.Text == "")
                commitMessageTextBox.Text = "Automatic commit";

            return true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (FormIsValid())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            using (var browser = new FolderBrowserDialog())
            {
                browser.ShowNewFolderButton = true;
                browser.RootFolder = Environment.SpecialFolder.MyDocuments;

                if (browser.ShowDialog(this) == DialogResult.OK)
                {
                    folderTextBox.Text = browser.SelectedPath;

                    if (nameTextBox.Text == "")
                        nameTextBox.Text = Path.GetFileName(folderTextBox.Text);
                }
            }
        }
    }
}