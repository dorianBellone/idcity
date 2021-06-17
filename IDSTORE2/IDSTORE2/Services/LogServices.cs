using System;
using System.IO;

namespace IDSTORE2.Services
{
    public enum TypeLog
    {
        Add = 0,
        Get = 1,
        Update = 2,
        Delete = 3,
        Rename = 4,
        ArchivesFileDelete = 5,
        ArchivesFileUpdate = 6,
    };

    public static class LogServices
    {
        private static string LogPath = string.Empty;
        private static bool DebugMode = true;

        public static void WriteLog(TypeLog _typeLog, String _user, String _msg)
        {
            LogPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter writer = File.AppendText(Path.Combine(LogPath, "Log_IDStore.txt")))
                {
                    writer.WriteLine("-----------------------");
                    writer.Write(Environment.NewLine);
                    writer.Write("[{0} {1}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    writer.Write("\t");
                    writer.WriteLine(" {0} : {1} by {2}", _typeLog, _msg, _user);
                    writer.WriteLine("-----------------------");
                }
                if (DebugMode)
                    Console.WriteLine(" {0} : {1} by {2}", _typeLog, _msg, _user);
            }
            catch (Exception e)
            {
            }
        }
    }
}
