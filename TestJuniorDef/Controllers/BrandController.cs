﻿using apitest.Models;
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
                var model = _brandService.GetBrandPerPage(size, page);

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
        [HttpPost]
        public IActionResult InsertBrand(string email, string password, string BrandName, string Description)
        {
            Brand brand = new Brand()
            {
                BrandName = BrandName,
                Description = Description,
                Account = new Account()
                {
                    Email = email,
                    Password = Encoding.ASCII.GetBytes(password),
                    AccountType = 1
                }
            };
            return StatusCode(_brandService.InsertBrand(brand));
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

}
    }
