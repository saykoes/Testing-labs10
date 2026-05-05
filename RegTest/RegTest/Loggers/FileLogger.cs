using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegTest.Log
{
    public class FileLogger : ILogger
    {
        public string LoggerFilePath { get; init; }
        ConsoleLogger consoleLogger = new ConsoleLogger(); // fallback

        public FileLogger(string loggerFilePath)
        {
            LoggerFilePath = loggerFilePath;

            // Опционально: создаем пустой файл при старте, если его нет
            if (!File.Exists(LoggerFilePath))
            {
                File.WriteAllText(LoggerFilePath, $"--- Log Started: {DateTime.Now} ---{Environment.NewLine}");
            }
        }

        public void Log(string message, bool isError = false)
        {
            consoleLogger.Log(message, isError);

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string level = isError ? "ERROR" : "Log";
            string fullMessage = $"[{timestamp}][{level}] {message}";

            try
            {
                // Добавляем строку и символ переноса строки
                File.AppendAllText(LoggerFilePath, fullMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't log into file: {ex.Message}");
            }
        }
    }
}
