using apitest.Models;
using BusinessAccess.ModelAPI.InfoRequestModels;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI;

namespace BusinessAccess.Services.Interfaces
{
    public interface IInfoRequestService
    {
        IEnumerable<InfoRequest> GetInfoRequests();
        PagingModelAPI<InfoRequestPagingModelAPI> GetInfoRequestsPerPage(int size = 5, int page = 1, int brand = 0, string search = "", int productId = 0, bool orderdesc = false);
        GetInfoRequestByIdModelAPI GetInfoRequestById(int id);
    }
}
