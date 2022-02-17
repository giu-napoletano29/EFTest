using apitest.Models;
using Microsoft.AspNetCore.Http;
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
            _context.Remove(obj);
            _context.SaveChanges();
        }

        public IQueryable<InfoRequestReply> GetAll()
        {
            return _context.InfoRequestReplies.AsNoTracking();
        }

        /// <summary>
        /// Get all InfoRequest replies with tracking on the returned entities
        /// </summary>
        /// <param name="includeAll"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return an InfoRequest reply specified by its id with entities tracking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<InfoRequestReply> GetByIdTracked(int id)
        {
            return _context.InfoRequestReplies.Where(x => x.Id == id)
                                               .Include(x => x.InfoRequest)
                                               .Include(x => x.Account);
        }

        public void Insert(InfoRequestReply obj)
        {
            _context.InfoRequestReplies.Add(obj);
            _context.SaveChanges();
        }

        public void Update(InfoRequestReply obj)
        {
            _context.InfoRequestReplies.Update(obj);
            _context.SaveChanges();
        }
    }
}
