using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace PracticalAspNetCore
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var dicts = new Dictionary<string, string>()
                {
                    ["id"] = "10",
                    ["name"] = "dody gunawinata",
                    ["date"] = "2020/05/30",
                    ["date2"] = "2020-05-30",
                    ["guid"] = System.Guid.NewGuid().ToString(),
                    ["artist"] = "Simon & Garfunkel",
                    ["formula"] = "10 = 10 * 1"
                };

                using var queryString = new FormUrlEncodedContent(dicts);
                
                context.Response.Headers.Add("Content-Type", "text/html");
                await context.Response.WriteAsync($@"<html>
                <head>
                    <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.5/css/bulma.css"" />
                </head>
                <body class=""content"">
                <div class=""container"">
                <h1>Using FormUrlEncodedContent to get URL encoded string</h1>
                <strong>Input</strong>
                ");
                await context.Response.WriteAsync("<ul>");
                foreach(var k in dicts)
                {
                    await context.Response.WriteAsync($"<li>{k.Key} = {k.Value}</li>");
                }
                await context.Response.WriteAsync("</ul>");
                await context.Response.WriteAsync("<strong>Output</strong><br/>");
                await context.Response.WriteAsync(await queryString.ReadAsStringAsync());
                await context.Response.WriteAsync("</ul>");
                await context.Response.WriteAsync(@"</div></body></html>");
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