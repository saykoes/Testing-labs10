using RegTest.Core;
using RegTest.DataBase;
using RegTest.Log;
using RegTest.UI;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

internal class Program
{
    private static readonly string LogFilePath = "registration_log.txt";

    static void Main(string[] args)
    {
        // dependencies
        //ConsoleLogger logger = new ConsoleLogger();
        //FileLogger fileLog = new FileLogger(LogFilePath);
        SerilogLogger serLog = new SerilogLogger(LogFilePath);
        DataBase db = new DataBase();
        Regestrator reg = new Regestrator(serLog, db);

        MainUIControl mainUi = new MainUIControl();
        mainUi.Start(reg);
    }
  
}
