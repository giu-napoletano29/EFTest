using System.Collections.Generic;

namespace TestJuniorDef.Repositories.Interfaces
{
    public interface IGeneric<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Save(T obj);
    }
}
