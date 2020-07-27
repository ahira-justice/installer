using System;
using System.ComponentModel;
using System.IO;

namespace Installer
{

    public class PackageUninstaller {

        private BackgroundWorker _deleteWorker = new BackgroundWorker();
        public string _installPath;

        public int DirectorySize { get; private set; }
        public int TotalDeletedSize { get; private set; }

        public PackageUninstaller(string installPath) {
            _deleteWorker.DoWork += DeleteDirectory;
            _deleteWorker.WorkerReportsProgress = true;
            _deleteWorker.WorkerSupportsCancellation = true;

            _installPath = installPath;
        }

        private void DeleteDirectory(object sender, DoWorkEventArgs e) {
            try {
                DirectorySize = Directory.GetDirectories(_installPath).Length;

                TotalDeletedSize = 0;

                foreach (string directoryEntry in Directory.EnumerateDirectories(_installPath)) {
                    Directory.Delete(directoryEntry, true);

                    TotalDeletedSize += 1;

                    long percent = TotalDeletedSize * 100 / DirectorySize;
                    _deleteWorker.ReportProgress((int)percent);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }

}