using System.Diagnostics;

namespace la_mia_pizzeria_crud.CustomLoggers
{
    public class CustomFileLogger : ICustomLogger
    {
        public void WriteLog(string message)
        {
            File.AppendAllText("./CustomLoggers/pizzaControllerLogs.txt", $"LOG: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - {message}\n");
        }
    }
}
