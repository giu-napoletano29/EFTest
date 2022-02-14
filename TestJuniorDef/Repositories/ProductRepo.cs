using apitest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
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
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Products.Add(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

        public void Update(Product obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.ProductCategories.RemoveRange(_context.ProductCategories.Where(x => x.ProductId == obj.Id));
                _context.Products.Update(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public void Delete(Product obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.ProductCategories.RemoveRange(_context.ProductCategories.Where(x => x.ProductId == obj.Id));
                _context.Products.Remove(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }


        }

        public IQueryable<Product> GetByIdTracked(int id)
        {
            return _context.Products.Where(x => x.Id == id)
                                        .Include(x => x.InfoRequests)
                                            .ThenInclude(x => x.InfoRequestReply);
        }
    }
}
