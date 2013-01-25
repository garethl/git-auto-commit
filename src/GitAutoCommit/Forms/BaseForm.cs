using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using GitAutoCommit.Properties;

namespace GitAutoCommit.Forms
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();

            Font = FontHelper.DefaultGuiFont;

            if (IsRuntime)
                Icon = Resources.icon_16;
        }


        protected bool IsRuntime
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Runtime; }
        }
    }
}