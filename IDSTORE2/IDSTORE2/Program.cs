using System;
using System.Collections.Generic;
using System.Linq;
using IDSTORE2.Controllers;
using IDSTORE2.Data;
using IDSTORE2.Models;
using IDSTORE2.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IDSTORE2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("---- START IDSTORE ----");
            Console.WriteLine("---- By Ben&Dodo ----");
            Console.WriteLine("---- IDCity ----");
            Console.WriteLine("---- avec la participation de Romen ----");

            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            // Write in Console all environment variables
            var config = host.Services.GetRequiredService<IConfiguration>();
            foreach (var c in config.AsEnumerable())
            {
                Console.WriteLine(c.Key + " = " + c.Value);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls("http://idboard.net:45005/");
                });

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<APIContext>();
                    var logger = services.GetRequiredService<ILogger<FileController>>();
                    var config = services.GetRequiredService<IConfiguration>();
                    var env = services.GetRequiredService<IWebHostEnvironment>();
                    var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
                    var logServices = services.GetRequiredService<LogServices>();
                    Console.WriteLine("Création/Initilisation de la DB : début !");
                    DbInitializer.Initialize(logger, context, config, env, httpContextAccessor, logServices);
                    Console.WriteLine("Création/Initilisation de la DB : fin !");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }


    }

    public static class DbInitializer
    {
        public static void Initialize(ILogger<FileController> logger, APIContext context, IConfiguration config, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor,LogServices logServices)
        {
            context.Database.EnsureCreated();
            // Insert TypeLog if don't exist. 
            int countTypeLog = context.TypeLog.Count();
            if (countTypeLog != 11)
            {
                context.TypeLog.RemoveRange(context.TypeLog);
                Models.TypeLog typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 1;
                typeLog.Name = "Add";
                context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 2;
                typeLog.Name = "Get";
                context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 3;
                typeLog.Name = "Update";
                context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 4;
                typeLog.Name = "Delete";
                context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 5;
                typeLog.Name = "Rename";
                context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 6;
                typeLog.Name = "ArchivesFileDelete";
                context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 7;
                typeLog.Name = "ArchivesFileUpdate";
                context.TypeLog.Add(typeLog);
                context.SaveChanges();
            }

            // Look if File Exist
            var count = context.File.Count();
            if (count != 0)
            {
                context.File.RemoveRange(context.File);
                context.SaveChanges();
                //return;
            }
            FileController fileController = new FileController(logger, context, config, env, logServices);
            String path = String.Empty;
            if (env.IsProduction())
            {
                path = config.GetSection("PathFile").GetSection("PathFileProd").Value;
            }
            else if (env.IsDevelopment())
            {
                path = config.GetSection("PathFile").GetSection("PathFileDev").Value;
            }
            IEnumerable<File> Files = FileServices.GetAllFile(path);
            List<Tag> tags = new List<Tag>();
            tags = context.Tag.ToList();
            foreach (File f in Files)
            {
               var file = new File { Name = f.Name, Description = f.Description, Type = f.Type, Path = f.Path };
               context.File.Add(file);
               if(f.Tags.First() != null && !string.IsNullOrWhiteSpace(f.Tags.First().Name) && tags.FirstOrDefault(t => t.Name == f.Tags.First().Name) == null) {
                    context.Tag.Add(f.Tags.First());
               }
            }
            context.SaveChanges();

            var FilesDB = context.File.ToList();

        }
    }
}
