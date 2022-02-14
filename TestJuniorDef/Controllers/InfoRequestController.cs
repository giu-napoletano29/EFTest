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
using TestJuniorDef.Services.Interfaces;
using TestJuniorDef.ModelAPI.ProductModels;
using TestJuniorDef.ModelAPI.InfoRequestModels;

namespace TestJuniorDef.Controllers
{
    [ApiController]
    [Route("leeds")]
    public class InfoRequestController : ControllerBase
    {
        private readonly ILogger<InfoRequestController> _logger;
        private readonly IInfoRequestService _infoRequestService;

        public InfoRequestController(ILogger<InfoRequestController> logger, IInfoRequestService infoRequestService)
        {
            _logger = logger;
            _infoRequestService = infoRequestService;
        }

        /// <summary>
        /// Return a collection with all the InfoRequests present in the database 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">List of brands</see></returns>
        [HttpGet]
        public IEnumerable<InfoRequest> GetInfoRequests()
        {
            return _infoRequestService.GetInfoRequests();
        }

        /// <summary>
        /// Return informations about an InfoRequest by a specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="IActionResult">ActionResult</see> <br/> Id <br/> Product <br/> Name <br/> LastName <br/> Email <br/> Address <br/> Replies</returns>
        [HttpGet("{id}")]
        public IActionResult GetInfoRequestById(int id)
        {
            var infoRequest = _infoRequestService.GetInfoRequestById(id);

            return Ok(infoRequest);
        }

        /// <summary>
        /// Return informations about a brand with pagination
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="IActionResult">ActionResult</see> <br/> PageSize <br/> TotalElements <br/> NumPage <br/> BrandName <br/> Description <br/> ProductsId </returns>

        [HttpGet("page/{page}")]
        [HttpGet("page/{size?}/{page?}")]
        public IActionResult GetInfoRequestPerPage(int size = 5, int page = 1, int brand = 0, int productId = 0, string search = "", bool orderdesc = false)
        {
            if (size <= 0 || page < 1)
            {
                return ValidationProblem();
            }
            try
            {
                var model = _infoRequestService.GetInfoRequestsPerPage(size, page, brand, search, productId, orderdesc);

                return Ok(model);
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return UnprocessableEntity();
        }

    }
}
