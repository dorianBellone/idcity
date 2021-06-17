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
            //CreateHostBuilder(args).Build().Run();
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

                    Console.WriteLine("Création de la DB : début !");
                    DbInitializer.Initialize(logger, context, config, env, httpContextAccessor);
                    Console.WriteLine("Création de la DB : fin !");
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
        public static void Initialize(ILogger<FileController> logger, APIContext context, IConfiguration config, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            context.Database.EnsureCreated();
            // Insert TypeLog if don't exist. 
            int countTypeLog = context.TypeLog.Count();
            if (countTypeLog != 7)
            {
                context.TypeLog.RemoveRange(context.TypeLog);


                Models.TypeLog typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 1;
                typeLog.Name = "Add";
                var result = context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 2;
                typeLog.Name = "Get";
                result = context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 3;
                typeLog.Name = "Update";
                result = context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 4;
                typeLog.Name = "Delete";
                result = context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 5;
                typeLog.Name = "Rename";
                result = context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 6;
                typeLog.Name = "ArchivesFileDelete";
                result = context.TypeLog.Add(typeLog);
                context.SaveChanges();
                typeLog = new Models.TypeLog();
                typeLog.TypeLogId = 7;
                typeLog.Name = "ArchivesFileUpdate";
                result = context.TypeLog.Add(typeLog);
                context.SaveChanges();
            }

            // Look if File Exist
            var count = context.File.Count();
            if (count != 0)
            {
                context.File.RemoveRange(context.File);
                //return;   // DB has been seeded
            }
            FileController fileController = new FileController(logger, context, config, env);
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

            //foreach (FileOverride f in Files)
            //{
            //    var file = new File { Name = f.Name, Description = f.Description, Type = f.Type, Path = f.Path };
            //    context.File.Add(file);
            //}
            //context.SaveChanges();

            //var FilesDB = context.Files;
            //Dictionary<Guid, String> dico = new Dictionary<Guid, string>(Files.Count);
            //foreach (File _file in Files)



        }
    }
}
