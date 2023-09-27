using BusinessWCF.DataAccess.ContextDB;

namespace BusinessWCF.DataAccess.Factory
{
    public interface IContextDBFactory
    {
        TechnicalExamDBContext Process();
    }
}
