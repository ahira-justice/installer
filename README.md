# installer

installer is a .NET Standard Class Library for installing archive packages.

## Usage

Download the repo as zip. Extract the contents into your solution directory. Add the installer project to your solution

```sh
$ dotnet sln <SLN_FILE> add Installer.csproj
```

## Interface

There are two classes in the Installer namespace. `PackageInstaller` and `PackageUninstaller`.

---

To begin an install to `installPath`

```sh
var installer = new PackageInstaller(archivePath, installPath)
installer.Extract()
```

`Extract()` accepts an optional `bool` keyword argument, `deleteOnComplete`. When set to `true`, the archive is deleted upon completing installation.

`installer.ArchiveSize` gets the size of the archive file in bytes.

`installer.TotalInstalledSize` gets the size of the installation at that instant in bytes. This value is updated as installation progresses.

---

To begin an uninstall from `installPath`

```sh
var uninstaller = new PackageUninstaller(installPath)
uninstaller.Uninstall()
```

`uninstaller.DirectorySize` gets the number of subdirectories at `installPath`.

`uninstaller.TotalDeletedSize` gets the number of subdirectories uninstalled at that instant. This value is updated as the uninstallation progresses.