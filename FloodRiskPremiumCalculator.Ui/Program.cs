using FloodRiskPremiumCalculator.Ui.Clients;
using FloodRiskPremiumCalculator.Ui.Configurations;
using Microsoft.Extensions.Options;

namespace FloodRiskPremiumCalculator.Ui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configurations = builder.Configuration;
            var services = builder.Services;

            // Add services to the container.
            services.AddControllersWithViews();

            services
                .AddOptions<BffClientConfig>()
                .Bind(configurations.GetSection(BffClientConfig.ConfigName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services
                .AddHttpClient<IBffClientService, BffClientService>((sp, httpClient) =>
                {
                    var config = sp.GetRequiredService<IOptions<BffClientConfig>>().Value;

                    httpClient.BaseAddress = new Uri(config.BaseUrl);
                });

            
            // Build pipeline
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
