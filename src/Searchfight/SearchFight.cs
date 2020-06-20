using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SF.Service.Api;
using SF.Service.Api.Engines;
using SF.Service.Api.Settings;
using SF.Service.Data;

namespace Searchfight {
    public class SearchFight {
        public static void Main(string[] args) {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<AppResult>().RunAsync(args).Wait();
            Console.ReadLine();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection) {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppResultSettings.json", false).Build();
            serviceCollection.AddOptions();
            serviceCollection.Configure<SearchSettings>(configuration.GetSection("SearchEngines"));
            serviceCollection.AddTransient<ISearchApi, GoogleSearchApi>();
            serviceCollection.AddTransient<ISearchApi, BingSearchApi>();
            serviceCollection.AddTransient<ISearcherFightService, SearcherFightService>();
            serviceCollection.AddTransient<AppResult>();
        }
    }
}