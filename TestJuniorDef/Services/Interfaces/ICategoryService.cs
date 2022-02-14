using apitest.Models;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.ModelAPI.InfoRequestModels;

namespace TestJuniorDef.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
