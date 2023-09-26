using Business.DataAccess.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Business.DataAccess.ContextDB
{
    public class TechnicalExamDBContext : DbContext
    {
        private readonly TechnicalExamContextOptions options;
        public TechnicalExamDBContext(TechnicalExamContextOptions _options) : base(_options.Options)
        { this.options = _options; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var mapping in options.Mappings)
            {
                mapping.Map(builder);
            }
            //options.DbContextSeed?.Seed(builder);
        }

    }

    public class TechnicalExamContextOptions
    {
        public readonly DbContextOptions<TechnicalExamDBContext> Options;
        public readonly IEnumerable<IEntityTypeMap> Mappings;

        public TechnicalExamContextOptions(DbContextOptions<TechnicalExamDBContext> options, IEnumerable<IEntityTypeMap> mappings)
        {
            Options = options;
            Mappings = mappings;
        }
    }

    public static class TechnicalExamContextExtensions
    {
        public static DbSet<TEntityType> DbSet<TEntityType>(this TechnicalExamDBContext context)
        where TEntityType : class
        {
            return context.Set<TEntityType>();
        }
    }


}
