using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public sealed class MenubarService : ServiceBase, IMenubarService
    {
        public MenubarService(KandandaDbContext dbContext) : base(dbContext) { }
        
        public void ResetDatabase()
        {
            Reset();
        }
    }
}
