using System;

namespace ReKatarina.Base
{
    internal static class Logger
    {
        #region Enumeration
        public enum LogType
        {
            Info,
            Warning,
            Error
        }
        #endregion
        private static string _lastLog = "";
        private static int _lastLogTick;

        /// <summary>
        ///     Print log
        /// </summary>
        /// <param name="info"> Log value </param>
        /// <param name="type"> Log type </param>
        public static void Print(string info, LogType type = LogType.Info)
        {
            // Prevent spamming with the same info
            if (info == _lastLog && Environment.TickCount - _lastLogTick < 500)
            {
                return;
            }

            Console.WriteLine($"[{type.ToString()} - {DateTime.Now:HH:mm:ss}] {info}");

            // Save last message and ticks
            _lastLog = info;
            _lastLogTick = Environment.TickCount;
        }
    }
}
