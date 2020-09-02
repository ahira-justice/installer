using System;
using System.ComponentModel;
using System.IO;
using Ionic.Zip;

namespace Installer {

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

        private void DeleteArchive(object sender, RunWorkerCompletedEventArgs e) {
            File.Delete(_archivePath);
        }

        private void ComponentExtractProgress(object sender, ExtractProgressEventArgs e) {
            if (e.TotalBytesToTransfer > 0) {
                long percent = e.BytesTransferred * 100 / e.TotalBytesToTransfer;
                _extractWorker.ReportProgress((int)percent);
            }
        }

        private void ExtractFile(object sender, DoWorkEventArgs e) {
            try {
                ArchiveSize = new FileInfo(_archivePath).Length;

                using (ZipFile zipFile = ZipFile.Read(_archivePath)) {
                    TotalInstalledSize = 0;

                    zipFile.ExtractProgress += ComponentExtractProgress;

                    foreach (ZipEntry zipEntry in zipFile) {
                        zipEntry.Extract(_installPath, ExtractExistingFileAction.OverwriteSilently);
                        TotalInstalledSize += zipEntry.CompressedSize;
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void Extract(bool deleteOnComplete = false) {
            _extractWorker.RunWorkerAsync();

            if (deleteOnComplete)
                _extractWorker.RunWorkerCompleted += DeleteArchive;
        }

    }

}
