using System;
using System.Linq;

namespace GitAutoCommit.Forms
{
    public partial class HeadingForm : BaseForm
    {
        public HeadingForm()
        {
            InitializeComponent();

            headingLabel.Font = FontHelper.HeadingGuiFont;
        }

        public override string Text
        {
            get { return headingLabel == null ? base.Text : headingLabel.Text; }
            set
            {
                base.Text = value;
                if (headingLabel != null)
                    headingLabel.Text = value;
            }
        }
    }
}