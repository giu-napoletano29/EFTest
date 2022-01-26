using apitest.Models;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.ModelAPI.CategoryModels;
using TestJuniorDef.ModelAPI.ProductModels;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Services.Interfaces;

namespace TestJuniorDef.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepo _brandRepo;
        private readonly ICategoryRepo _categoryRepo;
        public BrandService(IBrandRepo brandRepo, ICategoryRepo categoryRepo)
        {
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _brandRepo.GetAll();
        }

        public BrandModelAPI GetBrandById(int id)
        {
            var brand = _brandRepo.GetById(id);

            var cat = brand
                .SelectMany(x => x.Products
                    .SelectMany(y => y.ProductCategory
                        .Select(z => new CategoryModelAPI
                        {
                            Id = z.Category.Id,
                            CategoryName = z.Category.Name,
                            TotalProducts = x.Products.SelectMany(x => x.ProductCategory.Select(x => x.CategoryId)).Where(x => x == z.CategoryId).Count(),
                        })))
                .Distinct()
                .ToList();

            var brands = brand
                    .Select(x => new BrandModelAPI
                    {
                        Id = x.Id,
                        BrandName = x.BrandName,
                        TotalProducts = x.Products.Count,
                        TotalInfoRequest = x.Products.SelectMany(x => x.InfoRequests.Select(x => x.Id)).Count(),
                        Categories = cat,
                        Products = x.Products.Select(x => new ProductModelAPI
                        {
                            Id = x.Id,
                            ProductName = x.Name,
                            TotalInfoRequest = x.InfoRequests.Count
                        })
                    }).FirstOrDefault();
            
            return brands;
        }

        public PagingModelAPI<BrandPagingModelAPI> GetbrandPerPage(int size = 5, int page = 1)
        {
            var brands = _brandRepo.GetAll().Skip((size * page) - size).Take(size).Select(x => new BrandPagingModelAPI
            {
                BrandName = x.BrandName,
                Description = x.Description,
                ProductsId = x.Products.Select(y => y.Id).ToList()
            }).ToList();

            PagingModelAPI<BrandPagingModelAPI> model = new PagingModelAPI<BrandPagingModelAPI>();
            model.PageSize = size;
            model.TotalElements = _brandRepo.GetAll().Count();
            model.NumPage = page;
            model.Elements = brands;

            return model;
        }
    }
}
