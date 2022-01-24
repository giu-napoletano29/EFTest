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

namespace TestJuniorDef.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandRepo _brandRepo;

        public BrandController(Context context, ILogger<BrandController> logger, IBrandRepo brandRepo)
        {
            _context = context;
            _logger = logger;
            _brandRepo = brandRepo;
        }

        [HttpGet]
        public IEnumerable<Brand> GetBrands()
        {
            return _brandRepo.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetBrandById(int id)
        {
            var brand = _context.Brands
                .Where(x => x.Id == id);

            var cat = brand
                .SelectMany(x => x.Products
                    .SelectMany(y => y.ProductCategory
                        .Select(z => new CategoryModelAPI
                        {
                            Id = z.Category.Id,
                            CategoryName = z.Category.Name,
                            //TotalProducts = _context.ProductCategories.Where(x => x.CategoryId == z.CategoryId).Count()
                            //TotalProducts = z.Category.ProductCategory.Select(c => c.Product).Where(v => v.BrandId == id).Count(),
                            TotalProducts = x.Products.SelectMany(x => x.ProductCategory.Select(x => x.CategoryId)).Where(x => x == z.CategoryId).Count(),
                        })))
                .Distinct()
                .ToList();

            var brands = brand
                    .Select(x => new
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
                    }).ToList();
            return Ok(brands);
        }

        [HttpGet("test/{id}")]
        public IActionResult GetBrandByIdTest(int id)
        {
            var brandQuery =
                    from brand in _context.Brands
                    let p = brand.Products
                    let pc = p.SelectMany(x => x.ProductCategory)
                    //let cat = pc.Select(x => x.)
                    where brand.Id == id
                    select new
                    {
                        BrandId = brand.Id,
                        Brandname = brand.BrandName,
                        TotalProducts = brand.Products.Count,
                        TotalReq = brand.Products.SelectMany(x => x.InfoRequests.Select(x => x.Id)).Count(),
                        Categories = (from c in _context.Categories
                                      where pc.Select(x => x.CategoryId).Contains(c.Id)
                                      select new
                                      {
                                          Id = c.Id,
                                          CatName = c.Name,
                                          Totalprod = p.SelectMany(x => x.ProductCategory.Select(x => x.CategoryId)).Where(x => x == c.Id).Count()
                                      }).ToList(),

                        //Categories = pc.Select(z => new
                        //{
                        //    id = z.CategoryId,
                        //    CatName = z.Category.Name,
                        //    Totalprod = p.SelectMany(x => x.ProductCategory.Select(x => x.CategoryId)).Where(x => x == z.CategoryId).Count(), //duplicate to fix

                        //}).ToList(),
                        Products = p.Select(x => new ProductModelAPI
                        {
                            Id = x.Id,
                            ProductName = x.Name,
                            TotalInfoRequest = x.InfoRequests.Count

                        }).ToList(),


                    };




            return Ok(brandQuery);
        }

        [HttpGet("page/{size}/{page}")]
        public IActionResult GetbrandPerPage(int size, int page)
        {
          
            var brands = _context.Brands.Skip((size*page)-size).Take(size).Select(x => new BrandPagingModelAPI
            {
                BrandName = x.BrandName,
                Description = x.Description,
                ProductIds = x.Products.Select(y => y.Id).ToList()
            }).ToList();

            PagingModelAPI<BrandPagingModelAPI> view = new PagingModelAPI<BrandPagingModelAPI>();
            view.PageSize = size;
            view.TotalElements = _context.Brands.Count();
            view.NumPage = page;
            view.Elements = brands;
            
            return Ok(view);
        }

    }
}
