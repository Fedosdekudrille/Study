using System.Xml.Linq;

namespace Lec04LibN
{
    public class Logger : ILogger
    {
        private static string LogFileFolder = "Logs";
        private string LogFileName = string.Format(@"{0}/{1}/LOG{2}.txt", Directory.GetCurrentDirectory(), LogFileFolder,  DateTime.Now.ToString("yyyyMMdd-HH-mm-ss"));
        private int numOfLog = 0;
        private Logger()
        {
            log("INIT");
        }
        private static Logger instance;

        private string Title { get; set; } = "";
        private static object syncRoot = new();
        public static ILogger GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new();
                }
            }
            return instance;
        }
        public void log(string message)
        {
            numOfLog++;
            if (message == "START" || message == "STOP" || message == "INIT")
            {
                File.AppendAllText(LogFileName,
                numOfLog.ToString().PadLeft(6, '0') + '-' +
                DateTime.Now.ToString("yyyy-MMdd-HH-mm-ss") + " - " + message + "  " + Title + '\n');
            }
            else
            {
                File.AppendAllText(LogFileName,
                numOfLog.ToString().PadLeft(6, '0') + '-' +
                DateTime.Now.ToString("yyyy-MMdd-HH-mm-ss") + " - " + "INFO  " + Title + "  " + message + '\n');
            }

        }
        public void start(string title)
        {
            this.Title += title + ':';
            log("START");
        }

        public void stop()
        {
            Title = Title.Remove(Title.Length - 2, 2);
            log("STOP");
        }
    }
}