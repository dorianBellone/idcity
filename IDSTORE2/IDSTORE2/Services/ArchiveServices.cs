using IDSTORE2.Data;
using IDSTORE2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IDSTORE2.Services
{
    public enum TypeArchives
    {
        Update = 0,
        Delete = 1
    }

    public class ArchivesServices
    {
        private readonly LogServices logService;
        private readonly APIContext context;

        private string ArchivesPath { get; set; }
        private static bool DebugMode = true;

        private readonly IConfiguration config;
        private IWebHostEnvironment env;
        private Boolean testMode = false;
        
        public ArchivesServices(String _archivesPath)
        {
            ArchivesPath = _archivesPath;
            if (!System.IO.Directory.Exists(_archivesPath))
            {
                System.IO.Directory.CreateDirectory(_archivesPath);
            }
            testMode = true;
            logService = new LogServices();
            
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
            logService = new LogServices(context);
        }

        public async Task<Boolean> ArchiveFile(TypeArchives _typeArchives, String _filePath, String _classe, String _user)
        {

            if (!System.IO.File.Exists(_filePath) || String.IsNullOrWhiteSpace(_classe)) return false;
            if (String.IsNullOrWhiteSpace(_user) && DebugMode) _user = "adminModeDev";

            String nameFile = string.Empty;
            String type = string.Empty;
            String newPath = ArchivesPath;

            if (testMode || env.IsDevelopment())
            {
                nameFile = _filePath.Split('\\').Last().Split('.').First();
                type = Path.GetExtension(_filePath);
                newPath += _classe + "\\" + nameFile;
            }
            else if (env.IsProduction())
            {
                nameFile = _filePath.Split('/').Last();
                type = nameFile.Split('.').Last();
                nameFile = nameFile.Split('.').First();
                newPath += _classe + "/" + nameFile;
            }
            else return false;

            DirectoryInfo directoryToSearch = new DirectoryInfo(ArchivesPath + _classe);
            FileInfo[] filesInDir = directoryToSearch.GetFiles("*" + nameFile + "*.*");

            //foreach (FileInfo foundFile in filesInDir)
            //{
            //    string fullName = foundFile.FullName;
            //}
            if (filesInDir.Count() == 0)
            {
                if (String.IsNullOrWhiteSpace(type))
                    newPath += "_01";
                else
                    newPath += "_01" + "." + type;
            }
            else
            {
                if (filesInDir.Count() >= 9)
                {
                    if (String.IsNullOrWhiteSpace(type))
                        newPath += "_" + (filesInDir.Count() + 1).ToString();
                    else
                        newPath += "_" + (filesInDir.Count() + 1).ToString() + "." + type;
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(type))
                        newPath += "_" + (filesInDir.Count() + 1).ToString();
                    else
                        newPath += "_" + (filesInDir.Count() + 1).ToString() + "." + type;
                }
            }

            System.IO.File.Move(_filePath, newPath);

            //if (!testMode)
            //{
            var typeslog = logService.GetTypeLog();
            var typeLog = typeslog.FirstOrDefault(tl => tl.Name.Contains("ArchivesFile" + _typeArchives));
            //}

            // si typelog == update => Move le file dans les archive 
            if (typeLog.Name == "ArchivesFileUpdate")
            {
                String message = "ArchivesFileUpdate Fichier : " + _filePath + " To " + newPath + " By : " + _user;
                return await logService.AddLog(typeLog, _user, message);
            }
            else if (typeLog.Name == "ArchivesFileDelete")
            {
                String message = "ArchivesFileDelete Fichier : " + _filePath + " To " + newPath + " By : " + _user;
                return await logService.AddLog(typeLog, _user, message);
            }
            // si typelog == delete ==> Move le file dans les archive + log deletfileArhcive

            return false;


        }

       
    }
}
