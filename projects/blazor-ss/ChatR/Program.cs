using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ChatR.Services;
using Microsoft.Extensions.Logging;

namespace ChatR
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor(); //https://github.com/dotnet/aspnetcore/blob/master/src/Components/Server/src/DependencyInjection/ComponentServiceCollectionExtensions.cs
            services.AddScoped<NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(routes =>
            {
                routes.MapBlazorHub(); //https://github.com/dotnet/aspnetcore/blob/master/src/Components/Server/src/Builder/ComponentEndpointRouteBuilderExtensions.cs
                routes.MapHub<NotificationHub>("/notificationhub");
                routes.MapRazorPages();
                routes.MapFallbackToPage("/Index");
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
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureLogging(builder =>
                    {
                        builder.ClearProviders();
                        builder.SetMinimumLevel(LogLevel.Information);
                        builder.AddConsole();
                    });
                    webBuilder.UseEnvironment(Environments.Development);
                });
    }
}