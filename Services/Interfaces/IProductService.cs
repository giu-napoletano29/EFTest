using apitest.Models;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.ModelAPI.ProductModels;

namespace TestJuniorDef.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        ProductByIdModelAPI GetProductById(int id);
        PagingModelAPI<ProductPagingModelAPI> GetProductPerPage(int size = 5, int page = 1);
    }
}
