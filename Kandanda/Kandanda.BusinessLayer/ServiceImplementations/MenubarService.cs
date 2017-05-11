using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public sealed class MenubarService : ServiceBase, IMenubarService
    {
        public MenubarService(KandandaDbContextLocator contextLocator) : base(contextLocator)
        {
            
        }


        public void ResetDatabase()
        {
            KandandaDbContextLocator.ReInitializeDb(new SampleDataDbInitializer());
        }
    }
}
