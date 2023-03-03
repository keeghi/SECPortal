using System;

namespace SecPortal.Services.Managers.LoggerManager
{
    public interface ILoggerManager
    {
        void LogException(string caller, Exception ex, object param);
    }
}
