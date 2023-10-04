using BusinessWCF.DataAccess.Mappers;
using BusinessWCF.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessWCF.DataAccess.ContextDB
{
    public class TechnicalExamDBContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<User> users { get; set; }

        public TechnicalExamDBContext(string connectionString) 
        { 
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserMapper());
        }
    }
}
