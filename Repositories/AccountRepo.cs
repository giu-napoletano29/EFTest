using apitest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;

namespace TestJuniorDef.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly Context _context;

        public AccountRepo(Context context)
        {
            _context = context;
        }

        public IQueryable<Account> GetById(int id)
        {
            var account = _context.Accounts
                    //.Include(x => x.Brand)
                    //    .ThenInclude(x => x.Products)
                    //.Include(x => x.User)
                    .Where(x => x.Id == id).AsNoTracking();//.FirstOrDefault();

            return account;
        }

        public IQueryable<Account> GetAll()
        {
            return _context.Accounts.AsNoTracking();
        }

        public void Save(Account obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
