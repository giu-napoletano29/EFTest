using apitest.Models;
using System.Collections.Generic;

namespace TestJuniorDef.Services.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        Account GetAccountsById(int id);
        Account GetAccountsByUser(int id);
    }
}
