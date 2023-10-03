using System.Diagnostics;

namespace la_mia_pizzeria_static.CustomLoggers
{
    public class CustomFileLogger
    {
        public void WriteLog(string message)
        {
            File.AppendAllText("./CustomLoggers/pizzaControllerLogs.txt", $"LOG: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - {message}\n");
        }
    }
}
