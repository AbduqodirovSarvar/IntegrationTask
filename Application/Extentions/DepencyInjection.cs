using Application.Common.Interfaces;
using Application.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extentions
{
    public static class DepencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DepencyInjection).Assembly);
            });

            services.AddScoped<IFileService, FileService>();

            var mappingconfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingconfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
