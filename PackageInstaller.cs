using System.ComponentModel;

namespace Installer
{

    public class PackageInstaller {

        private BackgroundWorker _extractWorker = new BackgroundWorker();
        private string _archivePath;
        private string _installPath;

        public long ArchiveSize { get; private set; }
        public long TotalInstalledSize { get; private set; }

        public PackageInstaller(string archivePath, string installPath) {
            _extractWorker.DoWork += ExtractFile;
            _extractWorker.WorkerReportsProgress = true;
            _extractWorker.WorkerSupportsCancellation = true;

            _archivePath = archivePath;
            _installPath = installPath;
        }

        private void ExtractFile(object sender, DoWorkEventArgs e) { }

    }

}