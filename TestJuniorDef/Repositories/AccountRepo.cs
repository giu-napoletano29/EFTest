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
            var account = _context.Accounts.Where(x => x.Id == id).AsNoTracking();

            return account;
        }

        public IQueryable<Account> GetAll()
        {
            return _context.Accounts.AsNoTracking();
        }

        public IQueryable<Account> GetAll(bool includeAll)
        {
            if (!includeAll)
            {
                return GetAll();
            }

            return _context.Accounts.Include(x => x.Brand)
                                        .ThenInclude(x => x.Products)
                                    .Include(x => x.User);
        }

        public void Insert(Account obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Account obj)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Account obj)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Account> GetByIdTracked(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
