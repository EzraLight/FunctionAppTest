using FunctionAppTest.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup(typeof(FunctionAppTest.Startup))]
namespace FunctionAppTest
{

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddHttpClient();

            
            string connectionString = Environment.GetEnvironmentVariable("AzureDB") ?? throw new InvalidOperationException("AzureDB ConnectionString environment variable not set");
            builder.Services.AddDbContext<AppDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
        }
    }
}
