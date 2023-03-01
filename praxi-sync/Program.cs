using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using creatio_manager.Services;
using praxi_sync;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IServiceProvider services = PraxiServiceProvider.ConfigureServices();

        var contactServices = services.GetService<IContactServices>();

        var asas = await contactServices?.SyncContactsMupiToCreatio(DateTime.Now.AddYears(-1));

        Console.WriteLine("Hello, World!");
        Console.ReadLine();
    }



}