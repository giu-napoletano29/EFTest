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
using TestJuniorDef.ModelAPI.InfoRequestModels;

namespace TestJuniorDef.Controllers
{
    [ApiController]
    [Route("categories")]
    public class CategoryController : GenericController
    {
        private readonly ILogger<InfoRequestController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<InfoRequestController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Return a collection with all the InfoRequests present in the database 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">List of brands</see></returns>
        [HttpGet]
        public IEnumerable<Category> GetInfoRequests()
        {
            return _categoryService.GetCategories();
        }
    }
}
