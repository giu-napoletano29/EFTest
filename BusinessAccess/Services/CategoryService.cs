using apitest.Models;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.ModelAPI.InfoRequestModels;
using TestJuniorDef.ModelAPI.ProductModels;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Services.Interfaces;

namespace TestJuniorDef.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryrepo;
        public CategoryService(ICategoryRepo categoryrepo)
        {
            _categoryrepo = categoryrepo;
        }

        /// <summary>
        /// Return a collection with all the categories present in the database 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">List of brands</see></returns>
        IEnumerable<Category> ICategoryService.GetCategories()
        {
            return _categoryrepo.GetAll();
        }
    }
}
