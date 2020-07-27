using System.ComponentModel;

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

        private void DeleteDirectory(object sender, DoWorkEventArgs e) { }

    }

}