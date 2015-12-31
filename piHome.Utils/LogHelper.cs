using System;

namespace piHome.Utils
{
    public static class LogHelper
    {
        //private static readonly ILog loggerError = LogManager.GetLogger("ERROR_LOGGER");
        //private static readonly ILog loggerInfo = LogManager.GetLogger("INFO_LOGGER");

        //public static void LogError(Exception ex)
        //{
        //    loggerError.Error(string.Empty, ex);
        //}
        
        //public static void LogErrorMessage(string errMsg)
        //{
        //    loggerError.Error(errMsg);
        //}

        public static void LogMessage(string format, params object[] args)
        {
            var message = string.Format(format, args);
            LogMessage(message);
        }

        public static void LogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
