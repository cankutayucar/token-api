using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SharedLibrary.CustomHttpClient
{
    public static class HttpClientInject
    {
        public static void AddCustomHttpClient(this IServiceCollection service)
        {
            service.AddHttpClient<TryProductService>(options =>
            {
                // appsetting.json get baseuri 
                options.BaseAddress = new Uri("baseUri https://localhost:7299/api/");
            });
        }
    }
}
