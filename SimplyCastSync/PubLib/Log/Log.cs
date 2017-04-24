using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.IO;

namespace SimplyCastSync.PubLib.Log
{
    /// <summary>
    /// Exception Body
    /// </summary>
    public class ExceptionBody
    {
        public DateTime ts;
        public ExceptionType et;
        public ExceptionSrc es;
        public string info;
    }

    /// <summary>
    /// log handler
    /// </summary>
    public class Log
    {
        /// <summary>
        /// buffer length
        /// </summary>
        private readonly static int MaxLogBufferLength = 2048;

        //log repository in cache
        private static BlockingCollection<ExceptionBody> _consolelog { get; set; } = new BlockingCollection<ExceptionBody>(new ConcurrentQueue<ExceptionBody>());
        private static BlockingCollection<ExceptionBody> _filelog { get; set; } = new BlockingCollection<ExceptionBody>(new ConcurrentQueue<ExceptionBody>());

        /// <summary>
        /// add exceptions to queues
        /// </summary>
        /// <param name="eb"></param>
        /// <param name="lt"></param>
        public static void AddExceptionLog(ExceptionBody eb, LogType lt)
        {
            try
            {
                //console
                if ((lt == LogType.Console) || (lt == LogType.Console_File))
                {
                    while (_consolelog.Count >= MaxLogBufferLength)
                    {
                        _consolelog.Take();
                    }
                    _consolelog.Add(eb);
                }
                //file
                if ((lt == LogType.File) || (lt == LogType.Console_File))
                {
                    while (_filelog.Count >= MaxLogBufferLength)
                    {
                        _filelog.Take();
                    }
                    _filelog.Add(eb);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(eb.ts.ToString("yyyy/MM/dd HH:mm:ss.fff") + "---" + System.Enum.GetName(typeof(ExceptionType), eb.et) + ": " + eb.info);
            }
        }

        /// <summary>
        /// output console log
        /// </summary>
        public static void ExportConsoleLog()
        {
            Console.BufferHeight = 2048;
            ExceptionBody eb;
            while (true)
            {
                try
                {
                    eb = _consolelog.Take();
                    if (eb.et == ExceptionType.Error)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (eb.et == ExceptionType.Warning)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (eb.et == ExceptionType.Notification)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine(eb.ts.ToString("yyyy/MM/dd HH:mm:ss.fff") + "---" + Enum.GetName(typeof(ExceptionType), eb.et) + ": " + eb.info);

                    if (eb.es == ExceptionSrc.Exit)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(eb.ts.ToString("yyyy/MM/dd HH:mm:ss.fff") + "---" + System.Enum.GetName(typeof(ExceptionType), eb.et) + ": " + eb.info);
                }
            }

        }

        /// <summary>
        /// output file log
        /// </summary>
        public static void ExportFileLog()
        {
            ExceptionBody eb;
            while (true)
            {
                try
                {
                    eb = _filelog.Take();

                    if (eb.es == ExceptionSrc.Exit)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(eb.ts.ToString("yyyy/MM/dd HH:mm:ss.fff") + "---" + System.Enum.GetName(typeof(ExceptionType), eb.et) + ": " + eb.info);
                }
            }

        }

    } //end of class
}
