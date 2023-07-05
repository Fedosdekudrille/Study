using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    static class KFADiscInfo
    {
        static DriveInfo[] driveInfo;
        public delegate void Log(string str);
        public static Log log;
        static KFADiscInfo()
        {
            driveInfo = DriveInfo.GetDrives();
            log = KFALog.Write;
        }
        public static void FreeSpace()
        {
            long availableSpace = 0;
            foreach (DriveInfo driveInfo in driveInfo)
            {
                availableSpace += driveInfo.AvailableFreeSpace;
            }
            Console.WriteLine(availableSpace);
            log("Проверено количество свободного места");
        }
        public static void FileSystemInfo()
        {
            foreach (DriveInfo driveInfo in driveInfo)
            {
                Console.WriteLine(driveInfo.DriveFormat);
            }
            log("Проверен тип файловой системы");
        }
        public static void Info()
        {
            foreach (DriveInfo driveInfo in driveInfo)
            {
                Console.WriteLine($"{driveInfo.Name} {driveInfo.TotalFreeSpace} {driveInfo.AvailableFreeSpace} {driveInfo.VolumeLabel}");
            }
            log("Получена информация о дисках");
        }
    }
}
