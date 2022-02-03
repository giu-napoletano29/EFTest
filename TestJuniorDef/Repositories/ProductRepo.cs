using apitest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;

namespace TestJuniorDef.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly Context _context;

        public ProductRepo(Context context)
        {
            _context = context;
        }
        public IQueryable<Product> GetById(int id)
        {
            return _context.Products.Where(x => x.Id == id).AsNoTracking(); //TODO: handle tracking
        }

        public IQueryable<Product> GetAll()
        {
            return _context.Products.AsNoTracking();
        }

        public IQueryable<Product> GetAll(bool includeAll)
        {
            if (!includeAll)
            {
                return GetAll();
            }

            return _context.Products.Include(x => x.ProductCategory)
                                        .ThenInclude(x => x.Category)
                                    .Include(x => x.Brand);
        }

        public void Insert(Product obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Product obj)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Product obj)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Product> GetByIdTracked(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
