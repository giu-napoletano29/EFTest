using apitest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using BusinessAccess.Services.Interfaces;

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
