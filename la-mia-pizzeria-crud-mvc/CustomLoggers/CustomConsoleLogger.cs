﻿using System.Diagnostics;

namespace la_mia_pizzeria_static.CustomLoggers
{
    public class CustomConsoleLogger
    {
        public void WriteLog(string message)
        {
            Debug.WriteLine($"LOG: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - {message}");
        }
    }
}
