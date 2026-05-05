using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RegTest.Log
{
    public class SerilogLogger : ILogger
    {
        private readonly Serilog.ILogger _logger;

        public SerilogLogger(string filePath)
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}][{Level:u3}] {Message:lj}{NewLine}")
                .WriteTo.File(filePath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}][{Level:u3}] {Message:lj}{NewLine}")
                .CreateLogger();
        }

        public void Log(string message, bool isError = false)
        {
            if (isError)
                _logger.Error(message);
            else
                _logger.Information(message);
        }
    }
}
