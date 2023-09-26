
using Business.DataAccess.ContextDB;

namespace Business.DataAccess.Factory
{
    public interface IContextDBFactory
    {
        TechnicalExamDBContext Process();
    }
}
