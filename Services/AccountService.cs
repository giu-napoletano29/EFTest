using apitest.Models;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Services.Interfaces;

namespace TestJuniorDef.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepo.GetAll();
        }

        public Account GetAccountsById(int id)
        {
            return _accountRepo.GetById(id).FirstOrDefault();
        }


        public Account GetAccountsByUser(int id)
        {
            return _accountRepo.GetAll().Where(x => x.User.Id == id).FirstOrDefault();
        }
    }
}
