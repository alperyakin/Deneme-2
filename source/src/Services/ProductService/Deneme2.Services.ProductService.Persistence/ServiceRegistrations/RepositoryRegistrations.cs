using Deneme2.Services.ProductService.Domain.Categories.Repositories;
using Deneme2.Services.ProductService.Domain.Products.Repositories;
using Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Repositories.Categories;
using Deneme2.Services.ProductService.Persistence.EntityFrameworkCore.Repositories.Products;

using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.Services.ProductService.Persistence.ServiceRegistrations;
internal static class RepositoryRegistrations
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services) =>
        services
            .AddScoped<ICategoryCommandRepository, EfCategoryCommandRepository>()
            .AddScoped<IProductCommandRepository, EfProductCommandRepository>()
            .AddScoped<IProductQueryRepository, EfProductQueryRepository>();
}
