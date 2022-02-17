using apitest.Models;
using BusinessAccess.ModelAPI.BrandModels;
using BusinessAccess.ModelAPI.CategoryModels;
using BusinessAccess.ModelAPI.ProductModels;
using BusinessAccess.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.Repositories.Interfaces;

namespace BusinessAccess.Services
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
                        Email = x.Account.Email,
                        Password = x.Account.Password,
                        Description = x.Description,
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
        public PagingModelAPI<BrandPagingModelAPI> GetBrandPerPage(int size = 5, int page = 1, string search = "")
        {
            var repo = _brandRepo.GetAll(true);
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.TrimStart();
                repo = repo.Where(x => x.BrandName.Contains(search));
            }
            var pagination = Service.PaginateEntity(repo, size, page);
            PagingModelAPI<BrandPagingModelAPI> model = new PagingModelAPI<BrandPagingModelAPI>();
            model.PageSize = pagination.PageSize;
            model.TotalElements = pagination.TotalElements;
            model.NumPage = pagination.NumPage;
            model.Elements = pagination.Elements.Select(x => new BrandPagingModelAPI
            {
                Id = x.Id,
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
                return StatusCodes.Status201Created;
            }
            catch (Exception ex)
            {
                return StatusCodes.Status500InternalServerError;
            }

        }

        /// <summary>
        /// Update a brand with the given infos
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public int UpdateBrand(Brand brand)
        {
            //if (_brandRepo.GetAll().Any(x => x.Account.Email == brand.Account.Email && x.Id != brand.Id))
            //{
            //    return StatusCodes.Status409Conflict;
            //}
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
        /// Delete a brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteBrand(int id)
        {
            var brand = _brandRepo.GetByIdTracked(id).FirstOrDefault();
            if (brand != null)
            {
                try
                {
                    brand.IsDeleted = true;
                    brand.Account.IsDeleted = true;
                    foreach (var p in brand.Products)
                    {
                        p.IsDeleted = true;
                        foreach (var i in p.InfoRequests)
                        {
                            i.IsDeleted = true;
                            foreach (var r in i.InfoRequestReply)
                            {
                                r.IsDeleted = true;
                            }
                        }
                    }
                    //_brandRepo.Delete(brand);
                    _brandRepo.Update(brand);
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
        /// Check if provided email is duplicate
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public bool CheckEmailDuplicate(Brand brand)
        {
            if (_brandRepo.GetAll().Where(x => x.Account.Email == brand.Account.Email).Any(x => x.Id != brand.Id))
            {
                return true;
            }

            return false;
        }
    }
}
