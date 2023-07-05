using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    static class KFAFileInfo
    {
        public delegate void Log(string str);
        public static Log log;
        static KFAFileInfo()
        {
            log = KFALog.Write;
        }
        public static void GetFullPath(string path)
        {
            try
            {
                FileInfo fileInfo = new(path);
                Console.WriteLine(fileInfo.DirectoryName);
                log("Получен путь к файлу");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
        public static void Info(string path)
        {
            try 
            { 
                FileInfo fi = new(path);
                Console.WriteLine($"{fi.Length} {fi.Extension} {fi.Name}");
                log("Получена информация о файле");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
}
        public static void DateInfo(string path)
        {
            try
            {
                FileInfo fi = new(path);
                Console.WriteLine($"{fi.CreationTime}   {fi.LastWriteTime}");
                log("Получена информация о создании и изменении файла");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
    }
}
