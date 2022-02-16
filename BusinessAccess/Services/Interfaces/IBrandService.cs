using apitest.Models;
using BusinessAccess.ModelAPI.BrandModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI;

namespace BusinessAccess.Services.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetBrands();
        BrandModelAPI GetBrandById(int id);
        PagingModelAPI<BrandPagingModelAPI> GetBrandPerPage(int size = 5, int page = 1, string search = "");
        int InsertBrand(Brand brand);
        int UpdateBrand(Brand brand);
        int DeleteBrand(int id);
        public bool CheckEmailDuplicate(Brand brand);
    }
}
