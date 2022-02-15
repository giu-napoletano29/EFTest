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
using System.Text;

namespace TestJuniorDef.Controllers
{
    [ApiController]
    [Route("brands")]
    public class BrandController : GenericController
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

        /// <summary>
        /// Return informations about a brand with pagination
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="IActionResult">ActionResult</see> <br/> PageSize <br/> TotalElements <br/> NumPage <br/> BrandName <br/> Description <br/> ProductsId </returns>

        [HttpGet("page/{page}")]
        [HttpGet("page/{size?}/{page?}")]
        public IActionResult GetBrandPerPage(int size = 5, int page = 1, string search = "")
        {
            if (size <= 0 || page < 1)
            {
                return ValidationProblem("Invalid page requested");
            }
            try
            {
                var model = _brandService.GetBrandPerPage(size, page, search);

                return Ok(model);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return UnprocessableEntity();
        }

        /// <summary>
        /// Insert a new Brand and Account into the database with the specified infos
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="BrandName"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [HttpPost("new")]      
        public IActionResult InsertBrand([FromBody] Brand brand)
        {
            BrandValidation(brand);
            if (ModelState.IsValid)
            {
                return StatusCode(_brandService.InsertBrand(brand));
            }
            else
            {
                return ValidationProblem("Sent an invalid object");
            }
        }

        /// <summary>
        /// Update a brand if id exist with the given infos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="brandname"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPut("{id}/edit")]
        public IActionResult UpdateBrand(int id, [FromBody] Brand brand)
        {
            BrandValidation(brand);
            if (ModelState.IsValid)
            {
                brand.Id = id;
                return StatusCode(_brandService.UpdateBrand(brand));
            }
            else
            {
                return ValidationProblem("Sent an invalid object");
            }
        }

        /// <summary>
        /// Delete the brand with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteBrand(int id)
        {

            return StatusCode(_brandService.DeleteBrand(id));
        }

        /// <summary>
        /// Return a list of brands that match the given name string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        [HttpGet("name/{name}")]
        public IActionResult GetBrandByName(string name)
        {
            return Ok(_brandService.GetBrands().Where(x => x.BrandName == name).ToList());
        }

    }
}
