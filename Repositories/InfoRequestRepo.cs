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
        public InfoRequest GetById(int id)
        {
            return _context.InfoRequests
                    .Include(x => x.Product)
                        .ThenInclude(x => x.Brand)
                    .Include(x => x.InfoRequestReply)
                    .Include(x => x.User)
                    .Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<InfoRequest> GetAll()
        {
            return _context.InfoRequests.ToList();
        }

        public void Save(InfoRequest obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
