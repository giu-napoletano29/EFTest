using apitest.Models;
using Microsoft.AspNetCore.Http;
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

        public int DeleteAccount(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Return a collection with all the accounts present in the database 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepo.GetAll();
        }

        /// <summary>
        /// Return informations about an account by a specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account GetAccountsById(int id)
        {
            return _accountRepo.GetById(id).FirstOrDefault();
        }

        /// <summary>
        /// Return informations about an account by its linked user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account GetAccountsByUser(int id)
        {
            return _accountRepo.GetAll().Where(x => x.User.Id == id).FirstOrDefault();
        }

        public int InsertAccount(Account account)
        {
            try
            {
                _accountRepo.Insert(account);
            }
            catch (System.Exception e)
            {
                return StatusCodes.Status500InternalServerError;
            }
            return StatusCodes.Status200OK;
        }

        public int UpdateAccount(Account account)
        {
            throw new System.NotImplementedException();
        }
    }
}
