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
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepo _productRepo;

        public ProductController(ILogger<ProductController> logger, IProductRepo productRepo)
        {
            _logger = logger;
            _productRepo = productRepo;
        }

        /// <summary>
        /// Return a collection with all the products present in the database 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">List of brands</see></returns>
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _productRepo.GetAll();
        }


        /// <summary>
        /// Return informations about a product by a specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="IActionResult">ActionResult</see> <br/> Id <br/> Name <br/> BrandName <br/> Categories <br/> TotalInfoRequestGuest <br/> TotalInfoRequestLogged <br/> InfoRequest</returns>

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var p = _productRepo.GetById(id)
                        .Select(x => new
                        {
                            Id = x.Id,
                            Name = x.Name,
                            BrandName = x.Brand.BrandName,
                            Categories = x.ProductCategory.Select(z => new
                            {
                                Id = z.Category.Id,
                                CategoryName = z.Category.Name
                            }),
                            TotalInfoRequestGuest = x.InfoRequests.Where(x => x.UserId == null).Count(),
                            TotalInfoRequestLogged = x.InfoRequests.Where(x => x.UserId != null).Count(),
                            InfoRequest = x.InfoRequests.OrderByDescending(y => y.InfoRequestReply.Max(y => y.InsertDate))  //Repeating Max()
                            .Select(x => new
                            {
                                Id = x.Id,
                                Name = x.UserId == null ? x.Name : x.User.Name,
                                Lastname = x.UserId == null ? x.LastName : x.User.LastName,
                                TotalReply = x.InfoRequestReply.Count(),
                                //DateLastReply = x.InfoRequestReply.Select(x => x.InsertDate).OrderByDescending(x => x).FirstOrDefault(),
                                DateLastReply = x.InfoRequestReply.Max(x => x.InsertDate),
                            }),
                        }).ToList();
                return Ok(p);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return UnprocessableEntity();
        }

        /// <summary>
        /// Return informations about a product with pagination
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="IActionResult">ActionResult</see> <br/> PageSize <br/> TotalElements <br/> NumPage <br/> Elements </returns>

        [HttpGet("page/{size}/{page}")]
        public IActionResult GetProductPerPage(int size, int page)
        {
          
            var products = _productRepo.GetAll().Skip((size*page)-size).Take(size).Select(x => new ProductPagingModelAPI
            {
                Id = x.Id,
                ProductName = x.Name,
                Description = x.ShortDescription
            }).ToList();

            PagingModelAPI<ProductPagingModelAPI> view = new PagingModelAPI<ProductPagingModelAPI>();
            view.PageSize = size;
            view.TotalElements = _productRepo.GetAll().Count();
            view.NumPage = page;
            view.Elements = products;
            
            return Ok(view);
        }

    }
}
