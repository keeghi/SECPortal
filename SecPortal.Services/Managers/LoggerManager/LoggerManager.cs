using SecPortal.Entities.Entities;
using SecPortal.Entities.Infrastructures;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SecPortal.Services.Managers.LoggerManager
{
    public class LoggerManager : ILoggerManager
    {
        private readonly IDataContext _context;

        public LoggerManager(IDataContext context)
        {
            _context = context;
        }

        public void LogException(string caller, Exception ex, object param)
        {
            throw new NotImplementedException();
        }

        //public void LogException(string caller, Exception ex, object param)
        //{
        //    try
        //    {
        //        var log = new ApplicationLog(caller, ex.ToString(), ex.StackTrace, param);
        //        var superadmin = _context.Users.Single(x => x.UserName.Equals("admin@superadmin.com", StringComparison.OrdinalIgnoreCase));
        //        //_context.Logs.Add(log);
        //        _context.SaveChanges(superadmin.Id);
        //    }
        //    catch (Exception)
        //    {
        //        // We ask the God of code to not let this block throw exception
        //    }
        //}
    }

    public class FileLoggerManager : ILoggerManager
    {
        private readonly IDataContext _context;

        public FileLoggerManager(IDataContext context)
        {
            _context = context;
        }

        public void LogException(string caller, Exception ex, object param)
        {
            try
            {
                var log = new ApplicationLog(caller, ex.ToString(), ex.StackTrace, param);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/attachments", "error_log.txt");

                var lines = $"================================ START LOG - ${DateTime.Now:G} ==================================================="
                  + Environment.NewLine +
                  $"{JsonSerializer.Serialize(caller)}"
                  + Environment.NewLine +
                  $"{JsonSerializer.Serialize(ex.ToString())}"
                  + Environment.NewLine +
                  $"{JsonSerializer.Serialize(ex.StackTrace)}"
                  + Environment.NewLine +
                  $"{JsonSerializer.Serialize(param)}"
                  + Environment.NewLine +
                  $"====================================== END LOG ==================================================="
                  + Environment.NewLine;

                if (!File.Exists(savePath))
                {
                    File.WriteAllText(savePath, lines);
                }
                else
                {
                    File.AppendAllText(savePath, lines);
                }
            }
            catch (Exception)
            {
                // We ask the God of code to not let this block throw exception
            }
        }
    }
}
