namespace Devager
{
    using System.IO;
    using System.Linq;

    public static class Drives
    {
        public static float GetFileSize(this string folder, string searchPattern)
        {
            var files = Directory.GetFiles(folder, searchPattern, SearchOption.AllDirectories);
            var total = files.Select(item => new FileInfo(item)).Select(info => info.Length).Sum();

            return (total / 1024f) / 1024f;
        }

        public static float GetDriveTotalSize(this string driveName)
        {
            foreach (var drive in DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.Name == driveName))
            {
                return (drive.TotalSize / 1024f) / 1024f;
            }
            return -1;
        }

        public static float GetDriveTotalFreeSpace(this string driveName)
        {
            foreach (var drive in DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.Name == driveName))
            {
                return (drive.TotalFreeSpace / 1024f) / 1024f;
            }
            return -1;
        }

        public static long GetDriveStatus(this string driveName)
        {
            for (var index = 0; index < DriveInfo.GetDrives().Length; index++)
            {
                var drive = DriveInfo.GetDrives()[index];
                if (drive.IsReady && drive.Name == driveName)
                {
                    return ((drive.TotalSize - drive.TotalFreeSpace) * 100) / drive.TotalSize;
                }
            }
            return -1;
        }
    }
}
