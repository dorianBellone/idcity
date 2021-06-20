using IDSTORE2.Data;
using IDSTORE2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IDSTORE2.Services
{
    public class LogServices
    {
        private readonly APIContext context;

        public LogServices(APIContext _context)
        {
            context = _context;
        }
        public LogServices()
        {
            
        }
        public TypeLog GetTypeLog(string _name)
        {
            if (context == null || String.IsNullOrWhiteSpace(_name)) return null;

            return context.TypeLog.FirstOrDefault(tl => tl.Name.Contains(_name));

        }
        public IEnumerable<TypeLog> GetTypeLog()
        {
            if (context == null) {
                return new List<TypeLog>(){ new TypeLog(){TypeLogId = 6, Name = "ArchivesFileDelete" }, new TypeLog() { TypeLogId = 7, Name = "ArchivesFileUpdate" } };
            }
            return context.TypeLog.ToList();        
        }

        public async Task<Boolean> AddLog(TypeLog _typeLog, String _user, String _msg)
        {
            try
            {
                Log _log = new Log();
                _log.TypeLogID = _typeLog.TypeLogId;
                _log.DateTime = DateTime.Now;
                _log.User = _user;
                _log.Information = _msg;

                await context.Log.AddAsync(_log);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false; ;
            }
        }
        public int NumberLog()
        {
            return context.Log.Count();
        }
        public async Task<Boolean> RefineLog(DateTime? _dateTime)
        {
            try
            {
                if (!_dateTime.HasValue || _dateTime.Value == new DateTime())
                {
                    _dateTime = DateTime.Now.AddMonths(-3);
                }
                var result = context.Log.Where(lg => lg.DateTime < _dateTime).ToList();
                context.Log.RemoveRange(result);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
