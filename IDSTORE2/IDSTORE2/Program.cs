using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDSTORE2.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IDSTORE2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileController fc = new FileController();
            var toto = fc.Get();
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
