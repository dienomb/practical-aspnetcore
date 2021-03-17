using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles(); // By default this will server files out of wwwroot folder

            app.UseStaticFiles(new StaticFileOptions()
            {
                //The PhysialFileProvider will take any valid path. This way you can 
                //specify which folders will server the static files
                FileProvider =  new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot2")),
                RequestPath = new PathString("/2")
            });

            app.Run(async context =>
            {
                context.Response.Headers.Add("content-type", "text/html");

                await context.Response.WriteAsync(@"
                <html>
                <body>
                    From wwwroot</br>
                    <img src=""/kitty1.jpg""/>
                    <hr/>
                    From wwwroot2</br>
                    <img src=""/2/kitty2.jpg"" />
                </body>
                </html>
                ");
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}