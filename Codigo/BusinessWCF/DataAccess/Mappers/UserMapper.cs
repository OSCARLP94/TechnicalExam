using BusinessWCF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessWCF.DataAccess.Mappers
{
    public class UserMapper : BaseEntityMapper<User>
    {
        //para el mapeo de campos y caracteristicas de tabla USer
        protected override void InternalMap(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x=> x.Sex).IsRequired().HasMaxLength(1);
        }
    }
}
