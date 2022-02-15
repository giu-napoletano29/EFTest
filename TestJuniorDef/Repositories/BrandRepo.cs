﻿using apitest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
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

        /// <summary>
        /// Return a Brand specified by its id with entities tracking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Brand> GetByIdTracked(int id)
        {
            return _context.Brands.Where(x => x.Id == id)
                                    .Include(x => x.Account)
                                    .Include(x => x.Products)
                                        .ThenInclude(x => x.InfoRequests)
                                            .ThenInclude(x => x.InfoRequestReply);
        }


        public IQueryable<Brand> GetAll()
        {
            return _context.Brands.AsNoTracking();
        }

        /// <summary>
        /// Get all Brands with tracking on the returned entities
        /// </summary>
        /// <param name="includeAll"></param>
        /// <returns></returns>
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
                                            .ThenInclude(x => x.Category);
        }

        public int Insert(Brand obj)
        {
            using IDbContextTransaction transaction = _context.Database.BeginTransaction();   //TODO: dispose transactions
            try
            {
                _context.Brands.Add(obj);
                _context.SaveChanges();
                transaction.Commit();
                return StatusCodes.Status201Created;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return StatusCodes.Status500InternalServerError;
            }


        }

        public void Update(Brand obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Brands.Update(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

        public void Delete(Brand obj)
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
    }
}
