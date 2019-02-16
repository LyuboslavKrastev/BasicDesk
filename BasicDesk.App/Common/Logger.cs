using BasicDesk.App.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BasicDesk.App.Common
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            DateTime dateTimeNow = DateTime.Now;
            string fileName = $"Exception_{dateTimeNow.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)}.log";
            string logFolder = Directory.GetCurrentDirectory() + $@"\Logging\{dateTimeNow.ToString("dd-MM-yyyy",CultureInfo.InvariantCulture)}";
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            string logFilePath = $@"{logFolder}\{fileName}";          
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("----------------------------------------------------------------------------------------------------------");
            builder.AppendLine(dateTimeNow.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));
            builder.AppendLine(message);
            using(StreamWriter writer = new StreamWriter(logFilePath, true))
            {                
                writer.Write(builder.ToString());
                writer.Flush();
            }
        }
    }
}
