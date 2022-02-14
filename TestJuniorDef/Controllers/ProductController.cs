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
using TestJuniorDef.Services.Interfaces;
using TestJuniorDef.ModelAPI.ProductModels;

namespace TestJuniorDef.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Return a collection with all the products present in the database 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">List of brands</see></returns>
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetProducts();
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
                var p = _productService.GetProductById(id);
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

        [HttpGet("page/{page}")]
        [HttpGet("page/{size?}/{page?}")]
        public IActionResult GetProductPerPage(int size = 5, int page = 1, int brand = 0, bool orderbyBrand = false, bool orderbyName = false, bool orderbyPrice = false, bool desc = false )
        {
            if (size <= 0 || page < 1)
            {
                return ValidationProblem("Invalid page requested");
            }
            try
            {
                var retvalue = _productService.GetProductPerPage(size, page, brand, orderbyBrand, orderbyName, orderbyPrice, desc);

                return Ok(retvalue);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return UnprocessableEntity();
        }

        [HttpPost("new")]      
        public IActionResult InsertProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.InsertProduct(product);
            }
            else
            {
                return ValidationProblem("Sent an invalid object");
            }

            return StatusCode(200);
        }

        [HttpPut("{id}/edit")]       
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = id;
                return StatusCode(_productService.UpdateProduct(product));
            }
            else
            {
                return ValidationProblem("Sent an invalid object");
            }

        }

        /// <summary>
        /// Delete the product with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {

            return StatusCode(_productService.DeleteProduct(id));
        }
    }
}
