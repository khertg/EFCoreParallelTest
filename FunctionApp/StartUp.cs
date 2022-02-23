using DataLayer;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(FunctionApp.StartUp))]
namespace FunctionApp
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString", EnvironmentVariableTarget.Process)));
        }
    }
}
