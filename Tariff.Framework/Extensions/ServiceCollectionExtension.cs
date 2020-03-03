using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Tariff.Framework.Mappers;
using Tariff.Framework.Services.Interface;
using Tariff.Framework.Services;

namespace Banking.Framework.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTariffFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // Service
            services.AddScoped<ITariffService, TariffService>();

            // Mappers
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            return services;
        }
    }
}
