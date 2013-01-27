using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GitAutoCommit.Core;
using GitAutoCommit.Support;

namespace GitAutoCommit.Forms
{
    public partial class EditTaskForm : HeadingForm
    {
        private static readonly Interval[] _intervals = new[]
            {
                new Interval(5),
                new Interval(10),
                new Interval(15),
                new Interval(30),
                new Interval(60),
                new Interval(60*2),
                new Interval(60*5),
                new Interval(60*10),
                new Interval(60*15),
                new Interval(60*30),
                new Interval(60*60),
            };

        public EditTaskForm()
        {
            InitializeComponent();

            commitMessageTextBox.Font = FontHelper.MonospaceFont;

            intervalComboBox.Items.AddRange(_intervals);
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
            var interval = intervalComboBox.SelectedItem as Interval;
            item.SetProperties(nameTextBox.Text, folderTextBox.Text, commitMessageTextBox.Text, interval == null ? 30 : interval.Seconds);
        }

        private void Bind(AutoCommitTask item)
        {
            if (string.IsNullOrEmpty(item.Handler.Folder))
                Text = "add task";

            //normalises the line endings
            var commitMessage = item.CommitMessage
                .Replace("\r\n", "\n")
                .Replace("\r", "\n")
                .Replace("\n", "\r\n");

            nameTextBox.Text = item.Name;
            folderTextBox.Text = item.Folder;
            commitMessageTextBox.Text = commitMessage;

            if (nameTextBox.Text == "" && folderTextBox.Text != "")
                nameTextBox.Text = Path.GetFileName(folderTextBox.Text);

            if (commitMessageTextBox.Text == "")
                commitMessageTextBox.Text = "Automatic commit";

            intervalComboBox.SelectedItem = _intervals.FirstOrDefault(x => x.Seconds == item.Interval);
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
                browser.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

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