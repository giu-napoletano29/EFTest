using apitest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;

namespace TestJuniorDef.Repositories
{
    public class BrandRepo : IBrandRepo
    {
        private readonly Context _context;

        public BrandRepo(Context context)
        {
            _context = context;
        }
        public IQueryable<Brand> GetById(int id)
        {
            return _context.Brands.Where(x => x.Id == id).AsNoTracking();
        }


        public IQueryable<Brand> GetAll()
        {
            return _context.Brands.AsNoTracking();
        }        
        
        public IQueryable<Brand> GetAll(bool includeAll)
        {
            if (!includeAll)
            {
                return GetAll();
            }
            return _context.Brands.Include(x => x.Products)
                                        .ThenInclude(x => x.InfoRequests)
                                    .Include(x => x.Products)
                                        .ThenInclude(x => x.ProductCategory)
                                            .ThenInclude(x => x.Category); //TODO: try to generalize include
        }

        public void Save(Brand obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
