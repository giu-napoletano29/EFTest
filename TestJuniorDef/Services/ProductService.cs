using apitest.Models;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Return a collection with all the products present in the database 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            return _productRepo.GetAll();
        }

        /// <summary>
        /// Return informations about a product by a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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
                            DateLastReply = x.InfoRequestReply.Max(x => x.InsertDate),
                        }),
                    }).FirstOrDefault();

            return p;
        }

        /// <summary>
        /// Return informations about a product with pagination
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PagingModelAPI<ProductPagingModelAPI> GetProductPerPage(int size = 5, int page = 1, int brand = 0)
        {
            var repo = _productRepo.GetAll(true);
            if (brand > 0)
            {
                repo = repo.Where(x => x.BrandId == brand);
            }

            var pagination = Service.PaginateEntity<Product>(repo, size, page);
            PagingModelAPI<ProductPagingModelAPI> model = new PagingModelAPI<ProductPagingModelAPI>();
            model.PageSize = pagination.PageSize;
            model.TotalElements = pagination.TotalElements;
            model.NumPage = pagination.NumPage;
            model.Elements = pagination.Elements.Select(x => new ProductPagingModelAPI
            {
                Id = x.Id,
                BrandName = x.Brand.BrandName,
                ProductName = x.Name,
                Description = x.ShortDescription,
                Price = x.Price,
                Categories = x.ProductCategory.Select(z => new CategoryBaseModelAPI
                {
                    Id = z.Category.Id,
                    CategoryName = z.Category.Name
                }),
            }).ToList();

            return model;
        }

        public int InsertProduct(Product product)
        {
            try
            {
                _productRepo.Insert(product);
            }
            catch (System.Exception e)
            {
                return StatusCodes.Status500InternalServerError;
            }

            return StatusCodes.Status201Created;
        }

        public int UpdateProduct(Product product)
        {
            var prod = _productRepo.GetByIdTracked(product.Id).FirstOrDefault();
            if (prod != null)
            {
                try
                {
                    prod.Price = product.Price;
                    prod.Description = product.Description;
                    prod.BrandId = product.BrandId;
                    prod.Name = product.Name;
                    prod.ShortDescription = product.ShortDescription;
                    prod.ProductCategory = product.ProductCategory;

                    _productRepo.Update(prod);
                }
                catch (System.Exception e)
                {
                    return StatusCodes.Status500InternalServerError;
                }
            }
            else
            {
                return StatusCodes.Status404NotFound;
            }
            return StatusCodes.Status200OK;
        }

        public int DeleteProduct(int id)
        {
            var prod = _productRepo.GetById(id).FirstOrDefault();
            if (prod != null)
            {
                try
                {
                    _productRepo.Delete(prod);
                }
                catch (System.Exception e)
                {
                    return StatusCodes.Status500InternalServerError;
                }
            }
            else
            {
                return StatusCodes.Status404NotFound;
            }
            return StatusCodes.Status200OK;
        }
    }
}
