using CSharpEssentials.EntityFrameworkCore;
using Deneme2.BuildingBlocks.Database.PostgreSQL.Contexts;
using Deneme2.Services.StockService.Domain.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Contexts;
public sealed class ApplicationWriteDbContext : WriteDbContextBase<ApplicationWriteDbContext>
{
    public ApplicationWriteDbContext(
        DbContextOptions<ApplicationWriteDbContext> options,
        IServiceScopeFactory serviceScopeFactory) : base(options, serviceScopeFactory)
    {
    }

    public DbSet<Stock> Stocks => Set<Stock>();


}
