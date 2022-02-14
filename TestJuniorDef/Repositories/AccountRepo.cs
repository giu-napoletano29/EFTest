using apitest.Models;
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
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Accounts.Add(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public void Update(Account obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Accounts.Update(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public void Delete(Account obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Remove(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        public IQueryable<Account> GetByIdTracked(int id)
        {
            var account = _context.Accounts.Where(x => x.Id == id)
                                        .Include(x => x.Brand)
                                        .Include(x => x.User);

            return account;
        }
    }
}
