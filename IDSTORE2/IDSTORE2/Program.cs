using System;
using System.Collections.Generic;
using System.Linq;
using IDSTORE2.Controllers;
using IDSTORE2.Models;
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
            //CreateDbIfNotExists(host);
            // Write in Console all environment variables
            var config = host.Services.GetRequiredService<IConfiguration>();

            foreach (var c in config.AsEnumerable())
            {
                Console.WriteLine(c.Key + " = " + c.Value);
            }
            host.Run();
        }
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //var context = services.GetRequiredService<APIContext>();
                    var logger = services.GetRequiredService<ILogger<FileController>>();
                    var config = services.GetRequiredService<IConfiguration>();
                    var env = services.GetRequiredService<IWebHostEnvironment>();
                    var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();

                    Console.WriteLine("Création de la DB : début !");
                    DbInitializer.Initialize(logger/*, context*/, config, env, httpContextAccessor);
                    Console.WriteLine("Création de la DB : fin !");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls("http://idboard.net:45005/");
                });
    }

    public static class DbInitializer
    {
        public static void Initialize(ILogger<FileController> logger,/* APIContext context,*/ IConfiguration config, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            //context.Database.EnsureCreated();
            //// Look for any students.
            //var count = context.Files.Count();
            //if (count  != 0)
            //{
            //    context.Files.RemoveRange(context.Files);
            //    //return;   // DB has been seeded
            //}
            //FileController fc = new FileController(/*logger,context, */config, env /*httpContextAccessor*/);
            //var Files = fc.GetAll();
            //FileController fc = new FileController(logger,context, config, env);
            //var Files = fc.GetAll();
         
            /*foreach (FileOverride f in Files)
            {
                var file = new File { Name = f.Name, Description = f.Description, Type = f.Type, Path = f.Path };
                context.Files.Add(file);
            }
            context.SaveChanges();*/

            //var FilesDB = context.Files;
            //Dictionary<Guid, String> dico = new Dictionary<Guid, string>(Files.Count);
            //foreach (File _file in Files)
            
            
        }
    }
}
