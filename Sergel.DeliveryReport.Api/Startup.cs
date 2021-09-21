using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sergel.DeliveryReport.DataAccess.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Sergel.DeliveryReport.DataAccess;
using Microsoft.Azure.Cosmos;

[assembly: FunctionsStartup(typeof(Sergel.DeliveryReport.Api.Startup))]
namespace Sergel.DeliveryReport.Api
{
    public class Startup : FunctionsStartup
    {
        private IConfiguration _configuration;
        private DbConfig _dbConfig;
        private static string _deliveryReportContainer;
        private static string _moDeliveryReportContainer;
        private string _userContainer;

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            _configuration = builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .Build();
            //.AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
            //.AddEnvironmentVariables();  
            _dbConfig = _configuration.GetSection("CosmosDb").Get<DbConfig>();
            _deliveryReportContainer = _configuration.GetValue<string>("DeliveryReportContainer");
            _moDeliveryReportContainer = _configuration.GetValue<string>("MoDeliveryReportContainer");
            _userContainer = _configuration.GetValue<string>("UserContainer");
        }

        public override void Configure(IFunctionsHostBuilder app)
        {
            var cosmosDbClient = InitializeCosmosClientInstance(_dbConfig.Account, _dbConfig.Key);
            var dlReport = InitializeConfig(_dbConfig, cosmosDbClient, _deliveryReportContainer);
            var moDlReport = InitializeConfig(_dbConfig, cosmosDbClient,_moDeliveryReportContainer);
            var userConfig= InitializeConfig(_dbConfig,cosmosDbClient,_userContainer);

            app.Services.AddSingleton<IDeliveryReportRepository>(x => ActivatorUtilities.CreateInstance<DeliveryReportRepository>(x, dlReport));
            app.Services.AddSingleton<IMoDeliveryReportRepository>(x => ActivatorUtilities.CreateInstance<MoDeliveryReportRepository>(x, moDlReport));
            app.Services.AddSingleton<IUserRepository>(x => ActivatorUtilities.CreateInstance<UserRepository>(x, userConfig));
        }

        private static CosmosClient InitializeCosmosClientInstance(string account, string key)
        {
            return new Microsoft.Azure.Cosmos.CosmosClient(account, key);
        }

        private static DbConfig InitializeConfig(DbConfig dbConfig, CosmosClient cosmosClient,string containerName)
        {

            var deliveryReportConfig = new DbConfig
            {
                Account = dbConfig.Account,
                Key = dbConfig.Key,
                DbClient = cosmosClient,
                DatabaseName= dbConfig.DatabaseName,
                ContainerName = containerName
            };

            return deliveryReportConfig;
        }      
    }
}
