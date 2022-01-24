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

        public Account GetById(int id)
        {
            Account account = _context.Accounts.Include(x => x.Brand).ThenInclude(x => x.Products)
                                            .Include(x => x.User).Where(x => x.Id == id).FirstOrDefault();

            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public void Save(Account obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
