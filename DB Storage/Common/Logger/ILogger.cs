namespace Common.Logger
{
    internal interface ILogger
    {
        string FileName { get; }
        void Log(string message);
        //void Log(string message, NLog.LogLevel logLevel);
        void AddLogByFileName(string path);
        void RemoveLogByFileName(string path);
        void DefaultInfo(string message);
    }
}
