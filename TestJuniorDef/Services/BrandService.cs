﻿using apitest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public BrandService(IBrandRepo brandRepo, ICategoryRepo categoryRepo)
        {
            _brandRepo = brandRepo;
        }

        /// <summary>
        /// Return a collection with all the brands present in the database 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Brand> GetBrands()
        {
            return _brandRepo.GetAll();
        }

        /// <summary>
        /// Return informations about a brand by a specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return informations about a brand with pagination
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PagingModelAPI<BrandPagingModelAPI> GetBrandPerPage(int size = 5, int page = 1)
        {
            var repo = _brandRepo.GetAll(true);
            var pagination = Service.PaginateEntity<Brand>(repo, size, page);
            PagingModelAPI<BrandPagingModelAPI> model = new PagingModelAPI<BrandPagingModelAPI>();
            model.PageSize = pagination.PageSize;
            model.TotalElements = pagination.TotalElements;
            model.NumPage = pagination.NumPage;
            model.Elements = pagination.Elements.Select(x => new BrandPagingModelAPI
            {
                Id=x.Id,
                BrandName = x.BrandName,
                Description = x.Description,
                ProductsId = x.Products.Select(y => y.Id).ToList()
            }).ToList();

            return model;
        }
        /// <summary>
        /// Insert a new brand into the DataBase
        /// </summary>
        /// <param name="brand"></param>
        public int InsertBrand(Brand brand)
        {
            try
            {
                _brandRepo.Insert(brand);
            }
            catch (System.Exception e)
            {
                return StatusCodes.Status500InternalServerError;
            }

            return StatusCodes.Status201Created;
            
        }

        /// <summary>
        /// Update a brand with the given infos
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public int UpdateBrand(Brand brand)
        {
            var b = _brandRepo.GetByIdTracked(brand.Id).FirstOrDefault();
            if (b != null)
            {
                try
                {
                    b.Account.Email = brand.Account.Email;
                    b.Account.Password = brand.Account.Password;
                    b.BrandName = brand.BrandName;
                    b.Description = brand.Description;

                    _brandRepo.Update(b);
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

        /// <summary>
        /// Delete a brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteBrand(int id)
        {
            var brand = _brandRepo.GetById(id).FirstOrDefault();
            if (brand != null)
            {
                try
                {
                    _brandRepo.Delete(brand);
                }catch (System.Exception e)
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
