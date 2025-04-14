
using Application.Extentions;
using Infrastructure.Extentions;
using Web.Services;

namespace Web.Extentions
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddDepencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices(configuration);
            services.AddSingleton<SingleModalHelper>();
            services.AddSwagger();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Integration task Api",
                    Description = "An ASP.NET Core Web API for Synel Tesk Task Api items"
                });
            });
            return services;
        }
    }
}
