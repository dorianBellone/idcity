using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;

namespace IDSTORE2.Services
{
    public enum TypeArchives
    {
        Update = 0,
        Delete = 1
    }

    public class ArchivesServices
    {
        private string ArchivesPath { get; set; }
        private static bool DebugMode = true;

        private readonly IConfiguration config;
        private IWebHostEnvironment env;

        public ArchivesServices(String _archivesPath)
        {
            ArchivesPath = _archivesPath;
            if (!System.IO.Directory.Exists(_archivesPath))
            {
                System.IO.Directory.CreateDirectory(_archivesPath);
            }
        }

        public ArchivesServices(IConfiguration _config, IWebHostEnvironment _env)
        {
            config = _config;
            env = _env;

            if (env.IsProduction())
            {
                string PathProd = config.GetSection("PathFile").GetSection("PathArchivesProd").Value;
                ArchivesPath = PathProd;
            }
            else if (env.IsDevelopment())
            {
                string PathDev = config.GetSection("PathFile").GetSection("PathArchivesDev").Value;
                ArchivesPath = PathDev;
            }     
            if (!System.IO.Directory.Exists(ArchivesPath))
            {
                System.IO.Directory.CreateDirectory(ArchivesPath);
            }
        }

        public bool ArchiveFile(TypeArchives _typeArchives,String _filePath, String _classe)
        {
            if (!System.IO.File.Exists(_filePath))
                return false;

            String newPath = ArchivesPath;
            if (env.IsDevelopment())
            {
                newPath += _classe + "\\" + _filePath.Split('\\').Last();
            }
            else if (env.IsProduction())
            {
                newPath += _classe + "/" + _filePath.Split('/').Last();
            }


            File.Move(_filePath, ArchivesPath + "");





            return false;


        }
        //public  void WriteLog(TypeLog _typeLog, String _user, String _msg)
        //{
        //    LogPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //    try
        //    {
        //        using (StreamWriter writer = File.AppendText(Path.Combine(LogPath, "Log_IDStore.txt")))
        //        {
        //            writer.WriteLine("-----------------------");
        //            writer.Write(Environment.NewLine);
        //            writer.Write("[{0} {1}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
        //            writer.Write("\t");
        //            writer.WriteLine(" {0} : {1} by {2}", _typeLog, _msg, _user);
        //            writer.WriteLine("-----------------------");
        //        }
        //        if (DebugMode)
        //            Console.WriteLine(" {0} : {1} by {2}", _typeLog, _msg, _user);
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //}
    }
}
