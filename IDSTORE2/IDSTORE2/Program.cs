using System;
using System.Linq;
using IDSTORE2.Controllers;
using IDSTORE2.Models;
using Microsoft.AspNetCore.Hosting;
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

            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<APIContext>();
                    var logger = services.GetRequiredService<ILogger<FileController>>();

                    DbInitializer.Initialize(logger, context);
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
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class DbInitializer
    {
        public static void Initialize(ILogger<FileController> logger, APIContext context)
        {
            context.Database.EnsureCreated();
            // Look for any students.
            var count = context.Files.Count();
            if (count  != 0)
            {
                context.Files.RemoveRange(context.Files);
                //return;   // DB has been seeded
            }
            FileController fc = new FileController(logger,context);
            var Files = fc.Get();
         
            foreach (FileOverride f in Files)
            {
                var file = new File { Name = f.Name, Description = f.Description };
                context.Files.Add(file);
            }
            context.SaveChanges();
            
        }
    }
}
