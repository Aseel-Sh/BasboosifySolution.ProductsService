using Basboosify.BusinessLogicLayer.Mappers;
using Basboosify.BusinessLogicLayer.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Basboosify.BusinessLogicLayer.Validators;

namespace Basboosify.ProductsService.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        //TO DO: Add Data Access Layer service into IoC container

        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
        services.AddScoped<IProductService, Basboosify.BusinessLogicLayer.Services.ProductsService> ();
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        return services;
    }
}
