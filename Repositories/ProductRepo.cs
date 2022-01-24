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
        public Product GetById(int id)
        {
            return _context.Products
                        .Include(x => x.ProductCategory)
                            .ThenInclude(x => x.Category)
                        .Include(x => x.Brand)
                        .Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public void Save(Product obj)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable GetProductByIdAPIFormatting(int id)
        {
            return _context.Products
                        //.Include(x => x.ProductCategory)
                        //    .ThenInclude(x => x.Category)
                        //.Include(x => x.Brand)
                        .Where(x => x.Id == id)
                        .Select(x => new
                        {
                            Id = x.Id,
                            Name = x.Name,
                            BrandName = x.Brand.BrandName,
                            Categories = x.ProductCategory.Select(z => new
                            {
                                Id = z.Category.Id,
                                CategoryName = z.Category.Name
                            }),
                            TotalInfoRequestGuest = x.InfoRequests.Where(x => x.UserId == null).Count(),
                            TotalInfoRequestLogged = x.InfoRequests.Where(x => x.UserId != null).Count(),
                            InfoRequest = x.InfoRequests.OrderByDescending(y => y.InfoRequestReply.Max(y => y.InsertDate))  //Repeating Max()
                            .Select(x => new
                            {
                                Id = x.Id,
                                Name = x.UserId == null ? x.Name : x.User.Name,
                                Lastname = x.UserId == null ? x.LastName : x.User.LastName,
                                TotalReply = x.InfoRequestReply.Count(),
                                //DateLastReply = x.InfoRequestReply.Select(x => x.InsertDate).OrderByDescending(x => x).FirstOrDefault(),
                                DateLastReply = x.InfoRequestReply.Max(x => x.InsertDate),
                            }),
                        });
        }
    }
}
