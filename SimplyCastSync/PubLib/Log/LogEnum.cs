
namespace SimplyCastSync.PubLib.Log
{
    /// <summary>
    /// 
    /// </summary>
    public enum ExceptionType
    {
        Error = 0,
        Warning,
        Notification,
        Message,
        System
    }

    /// <summary>
    /// log type
    /// </summary>
    public enum LogType
    {
        Console = 0, //console
        File, //file
        Console_File //console & file
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ExceptionSrc
    {
        Init = 0,
        Processing,
        Cleanup,
        Exit
    }
}
