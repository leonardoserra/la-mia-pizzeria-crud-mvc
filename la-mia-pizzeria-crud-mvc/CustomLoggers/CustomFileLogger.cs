using System.Diagnostics;

namespace la_mia_pizzeria_static.CustomLoggers
{
    public class CustomFileLogger
    {
        public void WriteLog(string message)
        {
            File.AppendAllText("./PizzaControllerLogs.txt", message);
        }
    }
}
