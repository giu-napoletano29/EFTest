using apitest.Models;
using BusinessAccess.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;

namespace BusinessAccess.Services
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
