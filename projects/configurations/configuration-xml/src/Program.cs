using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class Startup
    {
        IConfigurationRoot _config;

        public Startup()
        {
            //This is the most basic configuration you can have
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("settings.xml");

            _config = builder.Build();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                foreach(var c in _config.AsEnumerable())
                {
                    await context.Response.WriteAsync($"{c.Key} = {_config[c.Key]}\n");
                }
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