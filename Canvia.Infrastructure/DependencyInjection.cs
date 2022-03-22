using Canvia.Infrastructure.Data;
using Canvia.Infrastructure.Queries;
using Canvia.Infrastructure.Queries.Interfaces;
using Canvia.Infrastructure.Repositories;
using Canvia.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ZP.SOOMMetrix.Performance.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RetoCanviaContext>(options => options.UseSqlServer(configuration.GetConnectionString("RetoCanviaEntities"), opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(30).TotalSeconds)));

            //Repositories
            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<IProductoRepository, ProductoRepository>();
            services.AddTransient<IPersonaRepository, PersonaRepository>();

            //Queries
            services.AddTransient<IProductoQuery, ProductoQuery>();
            services.AddTransient<IPersonaQuery, PersonaQuery>();

            return services;
        }
    }
}
