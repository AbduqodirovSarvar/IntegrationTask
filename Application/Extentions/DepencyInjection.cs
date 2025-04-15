using Application.Common.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extentions
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DepencyInjection).Assembly);
            });

            services.AddAutoMapper(typeof(DepencyInjection).Assembly);

            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
