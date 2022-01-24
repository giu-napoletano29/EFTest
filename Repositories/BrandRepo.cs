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
        public Brand GetById(int id)
        {
            return _context.Brands
                    .Include(x => x.Products)
                        .ThenInclude(x => x.InfoRequests)
                    .Include(x => x.Products)
                        .ThenInclude(x => x.ProductCategory)
                            .ThenInclude(x => x.Category).Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }

        public void Save(Brand obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
