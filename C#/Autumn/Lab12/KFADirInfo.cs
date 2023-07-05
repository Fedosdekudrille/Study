using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    static class KFADirInfo
    {
        public delegate void Log(string str);
        public static Log log;
        static KFADirInfo()
        {
            log = KFALog.Write;
        }
        public static void CountFiles(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new(path);
                Console.WriteLine(directoryInfo.GetFiles().Length);
                log("Получено точное количество файлов в папке");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
        public static void CreationTime(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new(path);
                Console.WriteLine(directoryInfo.CreationTime);
                log("Получено время создания папки");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
}
        public static void CountChildDirectories(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new(path);
                Console.WriteLine(directoryInfo.GetDirectories().Length);
                log("Получено количество подпапок");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
        public static void CountParentDirectories(string path)
        {
            try
            {
                byte numberOfParents = 0;
                foreach (char ch in path)
                {
                    if (ch == '\\')
                    {
                        numberOfParents++;
                    }
                }
                Console.WriteLine(numberOfParents - 1);
                log("Получено количество подпапок");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
    }
}
