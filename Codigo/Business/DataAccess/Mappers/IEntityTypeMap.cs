using Microsoft.EntityFrameworkCore;

namespace Business.DataAccess.Mappers
{
    public interface IEntityTypeMap
    {
        void Map(ModelBuilder builder);
    }

}
