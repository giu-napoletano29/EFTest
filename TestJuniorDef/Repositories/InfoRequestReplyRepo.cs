using apitest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;

namespace TestJuniorDef.Repositories
{
    public class InfoRequestReplyRepo : IInfoRequestReplyRepo
    {
        private readonly Context _context;

        public InfoRequestReplyRepo(Context context)
        {
            _context = context;
        }

        public void Delete(InfoRequestReply obj)
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

        public IQueryable<InfoRequestReply> GetAll()
        {
            return _context.InfoRequestReplies.AsNoTracking();
        }

        public IQueryable<InfoRequestReply> GetAll(bool includeAll)
        {
            if (!includeAll)
            {
                return GetAll();
            }

            return _context.InfoRequestReplies.Include(x => x.Account)
                                               .Include(x => x.InfoRequest);
        }

        public IQueryable<InfoRequestReply> GetById(int id)
        {
            return _context.InfoRequestReplies.Where(x => x.Id == id).AsNoTracking();
        }

        public IQueryable<InfoRequestReply> GetByIdTracked(int id)
        {
            return _context.InfoRequestReplies.Where(x => x.Id == id)
                                               .Include(x => x.InfoRequest)
                                               .Include(x => x.Account);
        }

        public void Insert(InfoRequestReply obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.InfoRequestReplies.Add(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public void Update(InfoRequestReply obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.InfoRequestReplies.Update(obj);
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
