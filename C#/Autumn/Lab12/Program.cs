namespace Lab12
{
    static class KFALog
    {
        public const string path = @"C:\Study\C#\Lab12\Log.txt";
        public static void Write(string logStr)
        {
            using (StreamWriter writer = new(path, true))
            {
                writer.WriteLine(DateTime.Now + " " + logStr);
            }
        }
        public static string Read()
        {
            string logs = "";
            using(StreamReader reader = new(path))
            {
                string[] strings = reader.ReadToEnd().Split('\n');
                string stringDate = "";
                bool isReadingData = true;
                foreach(string s in strings)
                {
                    if (s != "")
                    {
                        for (int i = 0; i < s.Length; i++)
                        {
                            if (s[i] == ' ')
                            {
                                if (isReadingData)
                                {
                                    isReadingData = false;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stringDate += s[i];
                        }
                        if(DateTime.Now.Subtract(Convert.ToDateTime(stringDate)) <= new TimeSpan(1, 0, 0))
                        {
                            logs += s + "\n";
                        }
                        isReadingData = true;
                        stringDate = "";
                    }
                }
                return logs;
            }
        }
        public static void FindBySubstr(string substr)
        {
            string[] logs = Read().Split('\n');
            foreach(string s in logs)
            {
                if (s.Contains(substr))
                {
                    Console.WriteLine(s);
                }
            }
        }
        public static void Rewrite()
        {
            string logs = Read();
            using(StreamWriter streamWriter = new(path))
            {
                streamWriter.Write(logs);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            KFADiscInfo.FreeSpace();
            KFADiscInfo.FileSystemInfo();
            KFADiscInfo.Info();

            Console.WriteLine();

            KFAFileInfo.GetFullPath(KFALog.path);
            KFAFileInfo.Info(KFALog.path);
            KFAFileInfo.DateInfo(KFALog.path);

            Console.WriteLine();

            string dirPath = @"C:\Study\C#\Lab12";

            KFADirInfo.CreationTime(dirPath);
            KFADirInfo.CountFiles(dirPath);
            KFADirInfo.CountParentDirectories(dirPath);

            Console.WriteLine();

            KFAFileManager.Inspect("C:\\");
            KFAFileManager.Rename(@"C:\Study\C#\Lab12\KFAInspect\kfadirinfo.txt", "dirinfo.txt");
            KFAFileManager.GetFilesByExtension(dirPath, "txt");
            KFAFileManager.ToZip(@"C:\Study\C#\Lab12\KFAInspect\KFAFiles");
            KFAFileManager.FromZip(@"C:\Study\C#\Lab12\KFAInspect\KFAFiles.zip", dirPath);

            KFALog.Rewrite();
            KFALog.FindBySubstr("Проверен");
        }
    }
}