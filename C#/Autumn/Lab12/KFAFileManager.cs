using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    static class KFAFileManager
    {
        public delegate void Log(string str);
        public static Log log;
        static KFAFileManager()
        {
            log = KFALog.Write;
        }
        public static void Inspect(string chosenDrive)
        {
            try
            {
                string[] directories = Directory.GetDirectories(chosenDrive);
                string[] files = Directory.GetFiles(chosenDrive);
                if (!Directory.Exists(@"C:\Study\C#\Lab12\KFAInspect"))
                    Directory.CreateDirectory(@"C:\Study\C#\Lab12\KFAInspect");
                using (StreamWriter streamWriter = new(@"C:\Study\C#\Lab12\KFAInspect\kfadirinfo.txt"))
                {
                    int strLength = 0;
                    streamWriter.WriteLine("Папки на диске:");
                    foreach (string directory in directories)
                    {
                        streamWriter.Write(directory + "  ");
                        strLength += directory.Length;
                        if (strLength > 50)
                        {
                            streamWriter.WriteLine();
                            strLength = 0;
                        }
                    }
                    strLength = 0;
                    streamWriter.WriteLine("\n\nФайлы на диске:");
                    foreach (string file in files)
                    {
                        streamWriter.Write(file + "  ");
                        strLength += file.Length;
                        if (strLength > 50)
                        {
                            streamWriter.WriteLine();
                            strLength = 0;
                        }
                    }
                }
                log("Получены файлы и папки из корневого каталога");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
        public static void Rename(string path, string newName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                string newPath = fileInfo.DirectoryName + "\\" + newName;
                if (File.Exists(newPath))
                {
                    File.Delete(newPath);
                }
                File.Copy(path, newPath);
                File.Delete(path);
                log("Переименован файл");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
        public static void GetFilesByExtension(string directoryPath, string extension)
        {
            try
            {
                string newDirectory = Path.Combine(directoryPath, "KFAFiles");
                if (Directory.Exists(newDirectory))
                {
                    Directory.Delete(newDirectory, true);
                }
                Directory.CreateDirectory(newDirectory);
                foreach (string file in Directory.GetFiles(directoryPath, "*." + extension))
                {
                    FileInfo fileInfo = new FileInfo(file);
                    fileInfo.CopyTo(Path.Combine(newDirectory, fileInfo.Name));
                }
                string newPath = Path.Combine(@"C:\Study\C#\Lab12\KFAInspect", "KFAFiles");
                if (Directory.Exists(newPath))
                {
                    Directory.Delete(newPath, true);
                }
                Directory.Move(newDirectory, newPath);
                log("Получены файлы с заданным разрешением");
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
        public static void ToZip(string directoryPath)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                string zipPath = Path.Combine(directoryInfo.Parent.FullName, "KFAFiles.zip");
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }
                ZipFile.CreateFromDirectory(directoryPath, zipPath);
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
        public static void FromZip(string zipPath, string targetPath)
        {
            try
            {
                string folderName = "";
                bool start = false;
                for (int i = zipPath.Length - 1; i >= 0; i--)
                {
                    if (start && zipPath[i] != '\\')
                    {
                        folderName += zipPath[i];
                    }
                    else if (zipPath[i] == '.')
                    {
                        start = true;
                    }
                    else if (start && zipPath[i] == '\\')
                    {
                        break;
                    }
                }
                folderName = new string(folderName.ToCharArray().Reverse().ToArray());
                Directory.CreateDirectory(Path.Combine(targetPath, folderName));
                ZipFile.ExtractToDirectory(zipPath, Path.Combine(targetPath, folderName));
            }
            catch
            {
                Console.WriteLine("По данному адресу ничего не найдено");
            }
        }
    }
}
