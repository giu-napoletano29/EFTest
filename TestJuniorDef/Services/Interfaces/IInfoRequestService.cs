using apitest.Models;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.ModelAPI.InfoRequestModels;

namespace TestJuniorDef.Services.Interfaces
{
    public interface IInfoRequestService
    {
        IEnumerable<InfoRequest> GetInfoRequests();
        PagingModelAPI<InfoRequestPagingModelAPI> GetInfoRequestsPerPage(int size = 5, int page = 1);
        GetInfoRequestByIdModelAPI GetInfoRequestById(int id);
    }
}
