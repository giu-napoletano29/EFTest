using apitest.Models;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI.InfoRequestModels;

namespace TestJuniorDef.Services.Interfaces
{
    public interface IInfoRequestService
    {
        IEnumerable<InfoRequest> GetInfoRequests();
        GetInfoRequestByIdModelAPI GetInfoRequestById(int id);
    }
}
