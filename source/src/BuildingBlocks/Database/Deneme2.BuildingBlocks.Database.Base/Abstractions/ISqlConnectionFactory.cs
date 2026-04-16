using System.Data;

namespace Deneme2.BuildingBlocks.Database.Base.Abstractions;
public interface ISqlConnectionFactory
{
    IDbConnection Create();
}
