using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDSTORE2.Controllers;
using IDSTORE2.Models;
using Microsoft.AspNetCore.Hosting;
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
                    DbInitializer.Initialize(context);
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
        public static void Initialize(APIContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Files.Any())
            {
                return;   // DB has been seeded
            }

            var Files = new File[]
            {
            new File{Name="Carson",Description="Alexander"},
            new File{Name="Meredith",Description="Alonso"},
            new File{Name="Arturo",Description="Anand"},
            new File{Name="Gytis",Description="Barzdukas"},
            new File{Name="Yan",Description="Li"},
            new File{Name="Peggy",Description="Justice"},
            new File{Name="Laura",Description="Norman"},
            new File{Name="Nino",Description="Olivetto"}
            };
            foreach (File f in Files)
            {
                context.Files.Add(f);
            }
            context.SaveChanges();

            //var courses = new Course[]
            //{
            //new Course{CourseID=1050,Title="Chemistry",Credits=3},
            //new Course{CourseID=4022,Title="Microeconomics",Credits=3},
            //new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
            //new Course{CourseID=1045,Title="Calculus",Credits=4},
            //new Course{CourseID=3141,Title="Trigonometry",Credits=4},
            //new Course{CourseID=2021,Title="Composition",Credits=3},
            //new Course{CourseID=2042,Title="Literature",Credits=4}
            //};
            //foreach (Course c in courses)
            //{
            //    context.Courses.Add(c);
            //}
            //context.SaveChanges();

            
        }
    }
}
