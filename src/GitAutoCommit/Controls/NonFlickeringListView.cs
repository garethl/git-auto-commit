using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GitAutoCommit
{
    public class NonFlickeringListView : ListView
    {
        private const int WM_LBUTTONDBLCLK = 0x203;
        private bool _disableDoubleClick;

        /// <summary>
        /// Disable the double click functionality
        /// </summary>
        [DefaultValue(false)]
        public bool DisableDoubleClick
        {
            get { return _disableDoubleClick; }
            set { _disableDoubleClick = value; }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr handle, int messg, int wparam, int lparam);

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // read current style
            var styles = (ListViewExtendedStyles)SendMessage(Handle, (int)ListViewMessages.GetExtendedStyle, 0, 0);
            // enable double buffer and border select
            styles |= ListViewExtendedStyles.DoubleBuffer | ListViewExtendedStyles.BorderSelect;
            // write new style
            SendMessage(Handle, (int)ListViewMessages.SetExtendedStyle, 0, (int)styles);
        }

        /// <summary>
        /// Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)"/>.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process.</param>
        protected override void WndProc(ref Message m)
        {
            //bypass the internal double click handling to disable auto-checking on double click...
            if (_disableDoubleClick && m.Msg == WM_LBUTTONDBLCLK)
            {
                OnMouseDoubleClick(new MouseEventArgs(MouseButtons, 1, MousePosition.X, MousePosition.Y, 0));
                return;
            }

            base.WndProc(ref m);
        }

        #region Nested type: ListViewExtendedStyles

        private enum ListViewExtendedStyles
        {
            /// <summary>
            /// LVS_EX_BORDERSELECT
            /// </summary>
            BorderSelect = 0x00008000,
            /// <summary>
            /// LVS_EX_DOUBLEBUFFER
            /// </summary>
            DoubleBuffer = 0x00010000,
        }

        #endregion

        #region Nested type: ListViewMessages

        private enum ListViewMessages
        {
            First = 0x1000,
            SetExtendedStyle = (First + 54),
            GetExtendedStyle = (First + 55),
        }

        #endregion
    }
}
