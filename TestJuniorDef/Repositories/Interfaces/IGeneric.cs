using System.Collections.Generic;
using System.Linq;

namespace TestJuniorDef.Repositories.Interfaces
{
    public interface IGeneric<T>
    {
        IQueryable<T> GetById(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(bool includeAll);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
