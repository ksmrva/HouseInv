using HouseInv.Api.Repositories.Configurations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HouseInv.Api.Repositories.Configurations.Cache
{
    public class DbSchemaAwareModelCacheKeyFactory : IModelCacheKeyFactory
    {
        public object Create(DbContext context, bool designTime)
        {
            return new
            {
                Type = context.GetType(),
                Schema = context is IDbContextSchema schema
                    ? schema.Schema
                    : null
            };
        }
    }
}