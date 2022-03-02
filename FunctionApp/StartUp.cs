using DataLayer;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;

[assembly: FunctionsStartup(typeof(FunctionApp.StartUp))]
namespace FunctionApp
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process);

            builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddTransient<IAssetRepository, AssetRepository>();

        }
    }
}
