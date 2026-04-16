using System.Data;
using Deneme2.BuildingBlocks.Database.Base.Abstractions;
using Npgsql;

namespace Deneme2.BuildingBlocks.Database.PostgreSQL;
internal sealed class SqlConnectionFactory
    (string connectionString) : ISqlConnectionFactory
{
    public IDbConnection Create() => new NpgsqlConnection(connectionString);
}
