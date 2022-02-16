using apitest.Models;
using System.Collections.Generic;

namespace BusinessAccess.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
