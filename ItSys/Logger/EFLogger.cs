using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ItSys.Logger
{
    public class EFLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new EFLogger();
        }

        public void Dispose()
        {

        }
    }

    public class EFLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var sqlContent = formatter(state, exception);
            //TODO: 拿到日志内容想怎么玩就怎么玩吧
            if (logLevel >= LogLevel.Information)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "SqlLog");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = Path.Combine(path, $"Log-{DateTime.Now.ToString("yyyyMMdd")}.log");
                using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        string logContent = $"**************{DateTime.Now.ToString()}**************\r\n" +
                            $"{logLevel.ToString()}\r\n" + sqlContent;
                        sw.WriteLine(logContent);
                    }

                }

            }
        }
    }
}
