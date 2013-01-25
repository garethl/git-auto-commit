using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GitAutoCommit.Core;

namespace GitAutoCommit.Controls
{
    public class AutoCommitList : EditableList<AutoCommitTask>
    {
        public void Bind(IList<AutoCommitTask> items)
        {
            Bind(items, CreateItem);
        }

        private ListViewItem CreateItem(AutoCommitTask task)
        {
            return new ListViewItem
                       {
                           Text = task.Name,
                           ImageKey = "ok"
                       };
        }
    }
}