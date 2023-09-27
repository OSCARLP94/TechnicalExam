using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessWCF.DataAccess.Mappers
{
    public abstract class BaseEntityMapper<TEntity> : IEntityTypeMap
         where TEntity : class
    {
        public void Map(ModelBuilder builder)
        {
            InternalMap(builder.Entity<TEntity>());
        }

        protected abstract void InternalMap(EntityTypeBuilder<TEntity> builder);
    }

}
