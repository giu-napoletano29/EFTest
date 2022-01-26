using apitest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Repositories;
using Microsoft.Data.SqlClient;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.ModelAPI.CategoryModels;
using TestJuniorDef.ModelAPI.ProductModels;
using TestJuniorDef.Services.Interfaces;

namespace TestJuniorDef.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandService _brandService;

        public BrandController(ILogger<BrandController> logger, IBrandService brandService)
        {
            _logger = logger;
            _brandService = brandService;
        }

        /// <summary>
        /// Return a collection with all the brands present in the database 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">List of brands</see></returns>
        [HttpGet]
        public IEnumerable<Brand> GetBrands()
        {
            return _brandService.GetBrands();
        }

        /// <summary>
        /// Return informations about a brand by a specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="IActionResult">ActionResult</see> <br/> BrandId <br/> BrandName <br/> TotalProducts <br/> TotalInfoRequest <br/> Categories <br/> Products</returns>
        [HttpGet("{id}")]
        public IActionResult GetBrandById(int id)
        {
            try
            {
                var brands = _brandService.GetBrandById(id);

                return Ok(brands);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return UnprocessableEntity();
        }

        //[HttpGet("test/{id}")]
        //public IActionResult GetBrandByIdTest(int id)
        //{
        //    try
        //    {
        //        var brandQuery =
        //                from brand in _brandRepo.GetAll()
        //                let p = brand.Products
        //                let pc = p.SelectMany(x => x.ProductCategory)
        //                //let cat = pc.Select(x => x.)
        //                where brand.Id == id
        //                select new
        //                {
        //                    BrandId = brand.Id,
        //                    Brandname = brand.BrandName,
        //                    TotalProducts = brand.Products.Count,
        //                    TotalReq = brand.Products.SelectMany(x => x.InfoRequests.Select(x => x.Id)).Count(),
        //                    Categories = (from c in _categoryRepo.GetAll()
        //                                  where pc.Select(x => x.CategoryId).Contains(c.Id)
        //                                  select new
        //                                  {
        //                                      Id = c.Id,
        //                                      CatName = c.Name,
        //                                      Totalprod = p.SelectMany(x => x.ProductCategory.Select(x => x.CategoryId)).Where(x => x == c.Id).Count()
        //                                  }).ToList(),

        //                //Categories = pc.Select(z => new
        //                //{
        //                //    id = z.CategoryId,
        //                //    CatName = z.Category.Name,
        //                //    Totalprod = p.SelectMany(x => x.ProductCategory.Select(x => x.CategoryId)).Where(x => x == z.CategoryId).Count(), //duplicate to fix

        //                //}).ToList(),
        //                Products = p.Select(x => new ProductModelAPI
        //                    {
        //                        Id = x.Id,
        //                        ProductName = x.Name,
        //                        TotalInfoRequest = x.InfoRequests.Count

        //                    }).ToList(),


        //                };
        //        return Ok(brandQuery);

        //    }
        //    catch (System.Exception e)
        //    {
        //        _logger.LogError(e, e.Message);
        //    }
        //    return UnprocessableEntity();
        //}


        /// <summary>
        /// Return informations about a brand with pagination
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="IActionResult">ActionResult</see> <br/> PageSize <br/> TotalElements <br/> NumPage <br/> BrandName <br/> Description <br/> ProductsId </returns>

        [HttpGet("page/{size?}/{page?}")]
        public IActionResult GetbrandPerPage(int size = 5, int page = 1)
        {
            if (size <= 0 || page < 1)
            {
                return ValidationProblem();
            }
            try
            {
                var model = _brandService.GetbrandPerPage(size, page);

                return Ok(model);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return UnprocessableEntity();
        }

    }
}
