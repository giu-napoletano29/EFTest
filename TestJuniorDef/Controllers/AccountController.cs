using apitest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Repositories;
using TestJuniorDef.Services.Interfaces;

namespace TestJuniorDef.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : GenericController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Return a collection with all the accounts present in the database 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">List of accounts</see></returns>
        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            return _accountService.GetAccounts();
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
                return _accountService.GetAccountsById(id);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return default;
        }

        /// <summary>
        /// Return informations about an account by its linked user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="IActionResult">ActionResult</see></returns>
        [HttpGet("user/{id}")]
        public Account GetAccountsByUser(int id)
        {
            try
            {
                return _accountService.GetAccountsByUser(id);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return default;
        }
    }
}
