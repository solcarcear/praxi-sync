using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using praxi_model;

using bussines_manager.Services;
using bussines_manager.Services.Imp;
using bussines_manager.DbContext;

using creatio_manager.HttpClients;
using creatio_manager.Services.Imp;
using creatio_manager.Services;

namespace praxi_sync
{
    internal static  class PraxiServiceProvider
    {
        public static IServiceProvider ConfigureServices()
        {

            var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json").Build();

            var appSettings = config.GetSection(nameof(AppSettings)).Get<AppSettings>() ?? new AppSettings();
            var authCreatio = new CreatioAuth(appSettings);
            var creatioUtils = new CreatioUtils();

            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton(appSettings)
            .AddSingleton(authCreatio)
            .AddSingleton(creatioUtils)
            .AddSingleton<IContactServices, ContactServices>()
            .AddSingleton<IMupiContactService, MupiContactService>()            
            .AddDbContext<MupiDbContext>(opts => opts.UseSqlServer(appSettings.MupiDbConnection));

            serviceProvider.AddHttpClient<ContactClient>("AccountClient", c =>
            {
                c.BaseAddress = new Uri($"{appSettings.UrlCreatio}");
                // Account API ContentType
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Add("ForceUseSession", "true");
                c.DefaultRequestHeaders.Add("BPMCSRF", authCreatio.CsrfToken);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    CookieContainer = authCreatio.AuthCookie
                };
            });

            serviceProvider.AddHttpClient<BatchClient>("BatchClient", c =>
            {
                c.BaseAddress = new Uri($"{appSettings.UrlCreatio}");
                c.Timeout = TimeSpan.FromHours(6);
                // Account API ContentType
                c.DefaultRequestHeaders.Add("ForceUseSession", "true");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Add("BPMCSRF", authCreatio.CsrfToken);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler()
                {
                    CookieContainer = authCreatio.AuthCookie
                };
            });

            return serviceProvider.BuildServiceProvider();
        }
    }
}
