using apitest.Models;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI;

namespace TestJuniorDef.Services.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetBrands();
        BrandModelAPI GetBrandById(int id);
        PagingModelAPI<BrandPagingModelAPI> GetBrandPerPage(int size = 5, int page = 1);
    }
}
