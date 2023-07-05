namespace Lab04;

class Program
{
    static void Main(string[] args)
    {
        ILogger logger1 = Logger.GetInstance();
        logger1.log("1");
        logger1.log("2");
        logger1.start("A");
        logger1.log("3");
        logger1.start("B");
        logger1.log("4");
        logger1.stop();
        logger1.log("5");

        ILogger logger2 = Logger.GetInstance();
        logger2.log("6");
        logger2.start("C");
        logger2.log("7");
        logger2.start("D");
        logger2.log("8");
        logger2.log("9");
        logger2.stop();
        logger2.log("10");
        logger2.log("11");
        logger2.log("12");
        logger2.stop();

        ILogger logger3 = Logger.GetInstance();
        logger3.log("13");
        logger3.start("E");
        logger3.start("F");
        logger3.stop();
        logger3.stop();
    }
}