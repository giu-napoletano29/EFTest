using apitest.Models;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.ModelAPI.CategoryModels;
using TestJuniorDef.ModelAPI.InfoRequestModels;
using TestJuniorDef.ModelAPI.ProductModels;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Services.Interfaces;

namespace TestJuniorDef.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepo.GetAll();
        }

        public ProductByIdModelAPI GetProductById(int id)
        {
            var p = _productRepo.GetById(id)
                    .Select(x => new ProductByIdModelAPI
                    {
                        Id = x.Id,
                        Name = x.Name,
                        BrandName = x.Brand.BrandName,
                        Categories = x.ProductCategory.Select(z => new CategoryBaseModelAPI
                        {
                            Id = z.Category.Id,
                            CategoryName = z.Category.Name
                        }),
                        TotalInfoRequestGuest = x.InfoRequests.Where(x => x.UserId == null).Count(),
                        TotalInfoRequestLogged = x.InfoRequests.Where(x => x.UserId != null).Count(),
                        InfoRequest = x.InfoRequests.OrderByDescending(y => y.InfoRequestReply.Max(y => y.InsertDate))  //Repeating Max()
                        .Select(x => new InfoRequestModelAPI
                        {
                            Id = x.Id,
                            Name = x.UserId == null ? x.Name : x.User.Name,
                            Lastname = x.UserId == null ? x.LastName : x.User.LastName,
                            TotalReply = x.InfoRequestReply.Count(),
                            //DateLastReply = x.InfoRequestReply.Select(x => x.InsertDate).OrderByDescending(x => x).FirstOrDefault(),
                            DateLastReply = x.InfoRequestReply.Max(x => x.InsertDate),
                        }),
                    }).FirstOrDefault();

            return p;
        }

        public PagingModelAPI<ProductPagingModelAPI> GetProductPerPage(int size = 5, int page = 1)  //TODO: generalize
        {
            var pagination = Service.PaginateEntity<Product>(_productRepo, size, page);
            PagingModelAPI<ProductPagingModelAPI> model = new PagingModelAPI<ProductPagingModelAPI>();
            model.PageSize = pagination.PageSize;
            model.TotalElements = pagination.TotalElements;
            model.NumPage = pagination.NumPage;
            model.Elements = pagination.Elements.Select(x => new ProductPagingModelAPI
            {
                Id = x.Id,
                ProductName = x.Name,
                Description = x.ShortDescription
            }).ToList();

            return model;
        }
    }
}
