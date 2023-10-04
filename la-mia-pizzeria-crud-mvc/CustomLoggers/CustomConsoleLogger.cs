using System.Diagnostics;

namespace la_mia_pizzeria_crud.CustomLoggers
{
    public class CustomConsoleLogger : ICustomLogger
    {
        public void WriteLog(string message)
        {
            Debug.WriteLine($"LOG: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - {message}");
        }
    }
}
