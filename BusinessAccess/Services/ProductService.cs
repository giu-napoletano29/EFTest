﻿using apitest.Models;
using BusinessAccess.ModelAPI.CategoryModels;
using BusinessAccess.ModelAPI.InfoRequestModels;
using BusinessAccess.ModelAPI.ProductModels;
using BusinessAccess.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.Repositories.Interfaces;

namespace BusinessAccess.Services
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
                        BrandId = x.BrandId,
                        BrandName = x.Brand.BrandName,
                        ShortDescription = x.ShortDescription,
                        Description = x.Description,
                        Price = x.Price,
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
        public PagingModelAPI<ProductPagingModelAPI> GetProductPerPage(int size = 5, int page = 1, int brand = 0, bool orderbyBrand = false, bool orderbyName = false, bool orderbyPrice = false, bool desc = false)
        {
            var repo = _productRepo.GetAll(true);
            if (brand > 0)
            {
                repo = repo.Where(x => x.BrandId == brand);
            }

            repo = repo.OrderBy(x => x.Brand.BrandName).ThenBy(x => x.Name);

            if (orderbyBrand)
            {
                repo = desc ? repo.OrderByDescending(x => x.Brand.BrandName) : repo.OrderBy(x => x.Brand.BrandName);
            }
            if (orderbyName)
            {
                repo = desc ? repo.OrderByDescending(x => x.Name) : repo.OrderBy(x => x.Name);
            }
            if (orderbyPrice)
            {
                repo = desc ? repo.OrderByDescending(x => x.Price) : repo.OrderBy(x => x.Price);
            }

            var pagination = Service.PaginateEntity(repo, size, page);
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
                return StatusCodes.Status201Created;
            }
            catch (Exception ex)
            {
                return StatusCodes.Status500InternalServerError;
            }
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
                catch (Exception e)
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

        /// <summary>
        /// Perform a logic deletion on a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteProduct(int id)
        {
            var prod = _productRepo.GetByIdTracked(id).FirstOrDefault();
            if (prod != null)
            {
                try
                {
                    prod.IsDeleted = true;
                    foreach (var i in prod.InfoRequests)
                    {
                        i.IsDeleted = true;
                        foreach (var r in i.InfoRequestReply)
                        {
                            r.IsDeleted = true;
                        }
                    }
                    //_productRepo.Delete(prod);
                    _productRepo.Update(prod);
                }
                catch (Exception e)
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
