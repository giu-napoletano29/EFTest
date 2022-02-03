using apitest.Models;
using Microsoft.EntityFrameworkCore;
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
                return GetAll();
            }

            return _context.InfoRequests.Include(x => x.User)
                                            .ThenInclude(x => x.Account)
                                        .Include(x => x.Product)
                                        .Include(x => x.InfoRequestReply)
                                        .Include(x => x.Nation);
        }

        public void Insert(InfoRequest obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update(InfoRequest obj)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(InfoRequest obj)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<InfoRequest> GetByIdTracked(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
