using Basboosify.DataAccessLayer.Context;
using Basboosify.DataAccessLayer.Repistories;
using Basboosify.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basboosify.ProductsService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //TO DO: Add Data Access Layer service into IoC container

        services.AddDbContext<ApplicationDbContext>
            (options =>
            {
                options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!);
            });

        services.AddScoped<IProductsRepository, ProductsRepository>();
        return services;
    }
}
