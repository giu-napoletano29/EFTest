using apitest.Models;
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

        public void Insert(InfoRequest obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.InfoRequests.Add(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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

        public IQueryable<InfoRequest> GetByIdTracked(int id)
        {
            return _context.InfoRequests.Where(x => x.Id == id)
                                        .Include(x => x.Product)
                                        .Include(x => x.User)
                                        .Include(x => x.InfoRequestReply);
        }
    }
}
