using apitest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
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

        /// <summary>
        /// Get all Categories with tracking on the returned entities
        /// </summary>
        /// <param name="includeAll"></param>
        /// <returns></returns>
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
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public void Update(Category obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public void Delete(Category obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Remove(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        /// <summary>
        /// Return a Category specified by its id with entities tracking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Category> GetByIdTracked(int id)
        {
            return _context.Categories
                        .Where(x => x.Id == id)
                        .Include(x => x.ProductCategory).ThenInclude(x => x.Product);
        }
    }
}
