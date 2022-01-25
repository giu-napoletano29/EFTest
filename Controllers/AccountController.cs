using apitest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Repositories;

namespace TestJuniorDef.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepo _accountRepo;

        public AccountController(ILogger<AccountController> logger, IAccountRepo accountRepo)
        {
            _logger = logger;
            _accountRepo = accountRepo;
        }

        /// <summary>
        /// Return a collection with all the accounts present in the database 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">List of accounts</see></returns>
        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepo.GetAll();
        }


        /// <summary>
        /// Return informations about an account by a specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="IActionResult">ActionResult</see></returns>

        [HttpGet("{id}")]
        public Account GetAccountsById(int id)
        {
            try
            {
                return _accountRepo.GetById(id).FirstOrDefault();
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return default;
        }

        //[HttpGet("user/{id}")]
        //public Account GetAccountsByUser(int id)
        //{
        //    try
        //    {
        //        return _accountRepo.GetById(id).FirstOrDefault();
        //    }
        //    catch (System.Exception e)
        //    {
        //        _logger.LogError(e, e.Message);
        //    }

        //    return default;
        //}
    }
}
