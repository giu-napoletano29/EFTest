using apitest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using TestJuniorDef.Repositories.Interfaces;

namespace TestJuniorDef.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly Context _context;

        public UserRepo(Context context)
        {
            _context = context;
        }
        public IQueryable<User> GetById(int id)
        {
            return _context.Users.Where(x => x.Id == id).AsNoTracking();
        }

        public IQueryable<User> GetByIdTracked(int id)
        {
            return _context.Users.Where(x => x.Id == id)
                                    .Include(x => x.Account)
                                    .Include(x => x.InfoRequests)
                                        .ThenInclude(x => x.InfoRequestReply);
        }


        public IQueryable<User> GetAll()
        {
            return _context.Users.AsNoTracking();
        }

        public IQueryable<User> GetAll(bool includeAll)
        {
            if (!includeAll)
            {
                return GetAll();
            }
            return _context.Users.Include(x => x.Account)
                                    .Include(x => x.InfoRequests)
                                        .ThenInclude(x => x.InfoRequestReply);
        }

        public void Insert(User obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Users.Add(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }


        }

        public void Update(User obj)
        {
            IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Users.Update(obj);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }

        public void Delete(User obj)
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
    }
}
