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
    public abstract class MenubarService : ServiceBase, IMenubarService
    {
        protected MenubarService(KandandaDbContext dbContext) : base(dbContext) { }

        public DbContext Current => DbContext;
        public void ResetDatabase()
        {
            Reset();
        }
    }
}
