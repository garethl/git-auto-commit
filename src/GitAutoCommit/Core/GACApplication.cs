using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace GitAutoCommit.Core
{
    [XmlType("git-auto-commit-settings")]
    public class GACApplication
    {
        private readonly static string SettingsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "git-auto-commit");
        private readonly static string SettingsFile = Path.Combine(SettingsDirectory, "settings.xml");

        public GACApplication()
        {
        }

        public GACApplication(bool startup)
        {
            if (Directory.Exists(SettingsDirectory) && File.Exists(SettingsFile))
            {
                var serializer = new XmlSerializer(typeof (GACApplication));

                try
                {
                    using (var file = new FileStream(SettingsFile, FileMode.Open, FileAccess.Read))
                    {
                        Tasks = ((GACApplication) serializer.Deserialize(file)).Tasks;
                        Tasks.Sort((x, y) => String.Compare(x.Name, y.Name, StringComparison.Ordinal));
                    }

                    foreach (var task in Tasks)
                    {
                        try
                        {
                            task.Handler.OnConfigurationChange();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            if (Tasks == null)
                Tasks = new List<AutoCommitTask>();
        }

        public GACApplication(bool isCommandLineDriven, IEnumerable<AutoCommitTask> tasks)
        {
            IsCommandLineDriven = isCommandLineDriven;
            Tasks = tasks.ToList();
        }

        [XmlIgnore]
        public bool IsCommandLineDriven { get; set; }

        [XmlElement("task")]
        public List<AutoCommitTask> Tasks { get; set; }

        public void Save()
        {
            Tasks.Sort((x, y) => String.Compare(x.Name, y.Name, StringComparison.Ordinal));

            var serializer = new XmlSerializer(typeof (GACApplication));

            if (!Directory.Exists(SettingsDirectory))
                Directory.CreateDirectory(SettingsDirectory);

            var tempFile = SettingsFile + ".temp";

            using (var file = new FileStream(tempFile, FileMode.Create, FileAccess.Write))
            {
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                serializer.Serialize(file, this, namespaces);
            }

            if (File.Exists(SettingsFile))
            {
                File.Replace(tempFile, SettingsFile, null, true);
            }
            else
            {
                File.Move(tempFile, SettingsFile);
            }
        }
    }
}
