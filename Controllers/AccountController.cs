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
        private readonly Context _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepo _accountRepo;

        public AccountController(Context context, ILogger<AccountController> logger, IAccountRepo accountRepo)
        {
            _context = context;
            _logger = logger;
            _accountRepo = accountRepo;
        }

        [HttpGet]
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepo.GetAll();
        }

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

        [HttpGet("user/{id}")]
        public Account GetAccountsByUser(int id)
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
    }
}
