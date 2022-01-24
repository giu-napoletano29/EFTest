using apitest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Repositories;
using Microsoft.Data.SqlClient;
using TestJuniorDef.ModelAPI;

namespace TestJuniorDef.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoRequestController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<InfoRequestController> _logger;

        public InfoRequestController(Context context, ILogger<InfoRequestController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<InfoRequest> GetInfoRequests()
        {
            return _context.InfoRequests;
        }

        [HttpGet("{id}")]
        public IActionResult GetInfoRequestById(int id)
        {
            var infoRequest = _context.InfoRequests
                    //.Include(x => x.Product)
                    //    .ThenInclude(x => x.Brand)
                    //.Include(x => x.InfoRequestReply)
                    //.Include(x => x.User)
                    .Where(x => x.Id == id)
                    .Select(x => new
                    {
                        Id = x.Id,
                        Product = new
                        {
                            Id = x.Product.Id,
                            Name = x.Product.Name,
                            Brandname = x.Product.Brand.BrandName
                        },
                        Name = x.UserId == null ? x.Name : x.User.Name,
                        Lastname = x.UserId == null ? x.LastName : x.User.LastName,
                        Email = x.UserId == null ? x.Email : x.User.Account.Email,
                        Address = x.City + " (" + x.PostalCode + "), " + x.Nation.Name,
                        Replies = x.InfoRequestReply.OrderByDescending(x => x.InsertDate).Select(x => new
                        {
                            Id = x.Id,
                            User = x.Account.AccountType == 1 ? x.Account.Brand.BrandName : x.Account.AccountType == 2 ? x.Account.User.Name + " " + x.Account.User.LastName : "Invalid User",
                            Text = x.ReplyText,
                            Date = x.InsertDate
                        })
                    }).ToList();

            return Ok(infoRequest);
        }

    }
}
