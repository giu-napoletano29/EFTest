using apitest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
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

        /// <summary>
        /// Get all Accounts with tracking on the returned entities
        /// </summary>
        /// <param name="includeAll"></param>
        /// <returns></returns>
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
            _context.Accounts.Add(obj);
            _context.SaveChanges();
        }

        public void Update(Account obj)
        {
            _context.Accounts.Update(obj);
            _context.SaveChanges();
        }

        public void Delete(Account obj)
        {
            _context.Remove(obj);
            _context.SaveChanges();
        }

        /// <summary>
        /// Return an Account specified by its id with entities tracking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Account> GetByIdTracked(int id)
        {
            var account = _context.Accounts.Where(x => x.Id == id)
                                        .Include(x => x.Brand)
                                        .Include(x => x.User);

            return account;
        }
    }
}
