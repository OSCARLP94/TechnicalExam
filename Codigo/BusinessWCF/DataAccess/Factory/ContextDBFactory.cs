using BusinessWCF.DataAccess.ContextDB;

namespace BusinessWCF.DataAccess.Factory
{
    public class ContextDBFactory : IContextDBFactory
    {
        private readonly TechnicalExamContextOptions options;

        public ContextDBFactory(TechnicalExamContextOptions options)
        {
            this.options = options;
        }
        public TechnicalExamDBContext Process()
        {
            return new TechnicalExamDBContext(options);
        }
    }

}
