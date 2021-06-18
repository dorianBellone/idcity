using IDSTORE2.Data;
using IDSTORE2.Models;
using System;
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
