using Microsoft.EntityFrameworkCore;

namespace BusinessWCF.DataAccess.Mappers
{
    public interface IEntityTypeMap
    {
        void Map(ModelBuilder builder);
    }

}
