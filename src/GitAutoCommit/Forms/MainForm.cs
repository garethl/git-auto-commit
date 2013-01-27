using System;
using System.Reflection;
using System.Windows.Forms;
using GitAutoCommit.Core;

namespace GitAutoCommit.Forms
{
    public partial class MainForm : HeadingForm
    {
        private static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

        private readonly GACApplication _application;

        public MainForm(GACApplication application)
        {
            _application = application;
            InitializeComponent();

            tasksLabel.Font = FontHelper.SubHeadingGuiFont;

            list.Bind(application.Tasks);
            list.ItemAdd += ListOnItemChange;
            list.ItemEdit += ListOnItemChange;
            list.ListChanged += ListOnChange;

            versionLabel.Text = Version;
        }

        private void ListOnChange(object sender, EventArgs e)
        {
            _application.Save();
        }

        private void ListOnItemChange(object sender, ValueEventArgs<AutoCommitTask> e)
        {
            var item = e.Value ?? new AutoCommitTask();

            using (var form = new EditTaskForm())
            {
                if (form.EditTask(item, this) == DialogResult.OK)
                {
                    e.Value = item;
                }
            }
        }
    }
}