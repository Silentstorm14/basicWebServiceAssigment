using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CustomerWebService.Models
{
    public static class LiteDbServiceExtention
    {
        public static void AddLiteDb(this IServiceCollection services, string databasePath)
        {
            services.AddTransient<CustomerContext, CustomerContext>();
            services.Configure<LiteDbConfig>(options => options.DatabasePath = databasePath);
        }
    }
}
