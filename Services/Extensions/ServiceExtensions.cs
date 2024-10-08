using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Categories;
using Services.ExceptionHandlers;
using Services.Filters;
using Services.Products;

namespace Services.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService,ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped(typeof(NotFoundFilter<,>));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();


        return services;
    }
}
