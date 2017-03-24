using System.Collections.Generic;
using Kandanda.Dal.Repository;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer
{
    public abstract class ServiceBase<T> where T : class, IEntry
    {
        protected Repository<T> Repository { get; }

        protected ServiceBase(Repository<T> repository)
        {
            Repository = repository;
        }

        public List<T> GetAll()
        {
            return Repository.GetAll();
        }

        public T GetById(int id)
        {
            return Repository.GetEntryById(id);
        }
    }
}