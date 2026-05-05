using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Deneme2.Services.StockService.Persistence.EntityFrameworkCore.Contexts;

/// <summary>
/// Design-time factory used by EF Core CLI tools.
/// </summary>
public sealed class ApplicationWriteDbContextFactory : IDesignTimeDbContextFactory<ApplicationWriteDbContext>
{
    public ApplicationWriteDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationWriteDbContext>();

        var connBuilder = new Npgsql.NpgsqlConnectionStringBuilder
        {
            Host = "localhost",
            Port = 5432,
            Database = "StockServiceDb"
        };
        
        optionsBuilder.UseNpgsql(connBuilder.ConnectionString);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        
        var serviceScopeFactory = serviceCollection.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

        return new ApplicationWriteDbContext(optionsBuilder.Options, serviceScopeFactory);
    }
}
