using apitest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;

namespace TestJuniorDef.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly Context _context;

        public CategoryRepo(Context context)
        {
            _context = context;
        }
        public IQueryable<Category> GetById(int id)
        {
            return _context.Categories
                        .Where(x => x.Id == id).AsNoTracking();
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories.AsNoTracking();
        }

        public IQueryable<Category> GetAll(bool includeAll)
        {
            if (!includeAll)
            {
                return GetAll();
            }

            return _context.Categories.Include(x => x.ProductCategory)
                                        .ThenInclude(x => x.Product);
        }

        public void Insert(Category obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Category obj)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Category obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
