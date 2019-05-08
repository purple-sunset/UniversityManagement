using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityManagement.Utilities
{
    public class Logger
    {
        public static void StartLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("D:\\UniversityManagement.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void LogInformation(string message)
        {
            Log.Information(message);
        }

        public static void LogInformation(Exception ex, string message)
        {
            Log.Information(ex, message);
        }

        public static void LogError(Exception ex)
        {
            Log.Error(ex, "Exception");
        }

        public static void LogError(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public static void EndLogger()
        {
            Log.CloseAndFlush();
        }
    }
}
