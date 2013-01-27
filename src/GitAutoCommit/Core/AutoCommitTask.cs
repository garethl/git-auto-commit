using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace GitAutoCommit.Core
{
    [XmlType("task")]
    public class AutoCommitTask
    {
        public AutoCommitTask()
        {
            Handler = new AutoCommitHandler();
        }

        public AutoCommitTask(int interval, string folder)
        {
            Handler = new AutoCommitHandler(interval, folder);
        }

        [XmlIgnore]
        public AutoCommitHandler Handler { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("folder")]
        public string Folder
        {
            get { return Handler.Folder; }
            set { Handler.Folder = value; }
        }

        [XmlAttribute("interval")]
        public int Interval
        {
            get { return Handler.Interval; }
            set { Handler.Interval = value; }
        }

        [XmlText]
        public string CommitMessage
        {
            get { return Handler.CommitMessage; }
            set { Handler.CommitMessage = value; }
        }

        public void SetProperties(string name, string folder, string commitMessage)
        {
            Name = name;
            Handler.SetProperties(folder, commitMessage, 30, true);
        }

        public bool IsValid()
        {
            return Directory.Exists(Folder) && Directory.Exists(Path.Combine(Folder, ".git"));
        }
    }
}