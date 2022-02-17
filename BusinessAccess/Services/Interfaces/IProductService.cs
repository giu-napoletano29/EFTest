using apitest.Models;
using BusinessAccess.ModelAPI.ProductModels;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI;

namespace BusinessAccess.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        ProductByIdModelAPI GetProductById(int id);
        PagingModelAPI<ProductPagingModelAPI> GetProductPerPage(int size = 5, int page = 1, int brand = 0, bool orderbyBrand = false, bool orderbyName = false, bool orderbyPrice = false, bool desc = false);
        int InsertProduct(Product product);
        int UpdateProduct(Product product);
        int DeleteProduct(int id);
    }
}
