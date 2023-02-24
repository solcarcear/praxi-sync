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

        var asas = await contactServices?.SyncContacts();

        Console.WriteLine("Hello, World!");
        Console.ReadLine();
    }



}