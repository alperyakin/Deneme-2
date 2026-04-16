using CSharpEssentials.EntityFrameworkCore;
using Deneme2.BuildingBlocks.Database.PostgreSQL.Contexts;
using Deneme2.Services.StockService.Domain.Stocks.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Contexts;
public sealed class ApplicationReadDbContext : ReadDbContextBase<ApplicationReadDbContext>
{
    public ApplicationReadDbContext(
        DbContextOptions<ApplicationReadDbContext> options,
        IServiceScopeFactory serviceScopeFactory) : base(options, serviceScopeFactory)
    {
    }

    public DbSet<StockReadModel> Stocks => Set<StockReadModel>();


}
