using apitest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;

namespace TestJuniorDef.Repositories
{
    public class InfoRequestRepo : IInfoRequestRepo
    {
        private readonly Context _context;

        public InfoRequestRepo(Context context)
        {
            _context = context;
        }
        public IQueryable<InfoRequest> GetById(int id)
        {
            return _context.InfoRequests
                    .Where(x => x.Id == id).AsNoTracking();
        }

        public IQueryable<InfoRequest> GetAll()
        {
            return _context.InfoRequests.AsNoTracking();
        }

        /// <summary>
        /// Get all InfoRequest with tracking on the returned entities
        /// </summary>
        /// <param name="includeAll"></param>
        /// <returns></returns>
        public IQueryable<InfoRequest> GetAll(bool includeAll)
        {
            if (!includeAll)
            {
                return _context.InfoRequests
                                        .Include(x => x.Product);
            }

            return _context.InfoRequests.Include(x => x.User)
                                            .ThenInclude(x => x.Account)
                                        .Include(x => x.Product)
                                            .ThenInclude(x => x.Brand)
                                        .Include(x => x.InfoRequestReply)
                                        .Include(x => x.Nation);
        }

        public int Insert(InfoRequest obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.InfoRequests.Add(obj);
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

        public void Update(InfoRequest obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.InfoRequests.Update(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public void Delete(InfoRequest obj)
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
        /// Return an InfoRequest specified by its id with entities tracking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<InfoRequest> GetByIdTracked(int id)
        {
            return _context.InfoRequests.Where(x => x.Id == id)
                                        .Include(x => x.Product)
                                        .Include(x => x.User)
                                        .Include(x => x.InfoRequestReply);
        }
    }
}
