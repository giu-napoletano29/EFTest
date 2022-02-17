﻿using apitest.Models;
using BusinessAccess.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;

namespace BusinessAccess.Services
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
            throw new NotImplementedException();
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
                return StatusCodes.Status201Created;
            }
            catch (Exception ex)
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        public int UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
