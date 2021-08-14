using Boleto.Service.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boleto.Service.API.DependencyInjectionConfig
{
    public static class DependencyInjection
    {
        public static void AddScoped(this IServiceCollection services, IConfiguration configuration)
        {
            #region Handlers
            services.AddTransient<BoletoHandler, BoletoHandler>();
            #endregion
        }
    }
}
