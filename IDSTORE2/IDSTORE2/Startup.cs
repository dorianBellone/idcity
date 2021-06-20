using IDSTORE2.Data;
using IDSTORE2.Models;
using IDSTORE2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace IDSTORE2
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            // SQL
            services.AddDbContext<APIContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("SQL_Connection")));
            // SQL LITE
            //services.AddDbContext<APIContext>(options =>
            //options.UseSqlite(Configuration.GetConnectionString("SQL_LiteConnection")));

            services.AddScoped<APIContext>();
            services.AddScoped<LogServices>();
            services.AddScoped<ArchivesServices>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();

            if (_env.IsDevelopment())
            {
                Console.WriteLine("----------");
                Console.WriteLine(_env.EnvironmentName);
                Console.WriteLine("----------");
                Console.WriteLine(_env.WebRootPath);

                Console.WriteLine("----------");
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });
                // ADD BR TEST uplaod 
                services.Configure<FormOptions>(o => {
                    o.ValueLengthLimit = int.MaxValue;
                    o.MultipartBodyLengthLimit = int.MaxValue;
                    o.MemoryBufferThreshold = int.MaxValue;
                });
            }
            if (_env.IsProduction())
            {
                Console.WriteLine("----------");
                Console.WriteLine(_env.EnvironmentName);
                
                Console.WriteLine("----------");

                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "wwwroot";
                });

                Console.WriteLine("----------");
                Console.WriteLine("c.RootPath = \"wwwroot\"    c'est fait !!!! ");
                Console.WriteLine("----------");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                if (_env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.Options.SourcePath = "ClientApp";
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    //// ADD By B.R 29/04 https://code-maze.com/upload-files-dot-net-core-angular/
                    app.UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider("C:\\RessourceFile"),
                        RequestPath = new PathString("/ResourcesFile")
                    });
                }

                if (_env.IsProduction())
                {
                    spa.Options.SourcePath = "wwwroot";
                    //// ADD By B.R 29/04 https://code-maze.com/upload-files-dot-net-core-angular/
                    //app.UseStaticFiles(new StaticFileOptions()
                    //{
                    //    FileProvider = new PhysicalFileProvider("/home/RessourceFile/"),
                    //    RequestPath = new PathString("/RessourceFile")
                    //});
                }

            });

            Console.WriteLine();
        }
    }
}
