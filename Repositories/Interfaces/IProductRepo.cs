using apitest.Models;
using System.Collections.Generic;
using System.Linq;

namespace TestJuniorDef.Repositories.Interfaces
{
    public interface IProductRepo : IGeneric<Product>
    {
        //IQueryable GetProductByIdAPIFormatting(int id);
    }
}
