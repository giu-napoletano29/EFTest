using apitest.Models;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.ModelAPI.InfoRequestModels;
using TestJuniorDef.ModelAPI.ProductModels;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Services.Interfaces;

namespace TestJuniorDef.Services
{
    public class InfoRequestService : IInfoRequestService
    {
        private readonly IInfoRequestRepo _inforeqrepo;
        public InfoRequestService(IInfoRequestRepo inforeqrepo)
        {
            _inforeqrepo = inforeqrepo;
        }

        public IEnumerable<InfoRequest> GetInfoRequests()
        {
            return _inforeqrepo.GetAll();
        }

        public GetInfoRequestByIdModelAPI GetInfoRequestById(int id)
        {
            var infoRequest = _inforeqrepo.GetById(id)
                .Select(x => new GetInfoRequestByIdModelAPI
                {
                    Id = x.Id,
                    Product = new ProductBasePlusBrandNameModelAPI
                    {
                        Id = x.Product.Id,
                        ProductName = x.Product.Name,
                        BrandName = x.Product.Brand.BrandName
                    },
                    Name = x.UserId == null ? x.Name : x.User.Name,
                    Lastname = x.UserId == null ? x.LastName : x.User.LastName,
                    Email = x.UserId == null ? x.Email : x.User.Account.Email,
                    Address = x.City + " (" + x.PostalCode + "), " + x.Nation.Name,
                    Replies = x.InfoRequestReply.OrderByDescending(x => x.InsertDate).Select(x => new ReplyModelAPI
                    {
                        Id = x.Id,
                        User = x.Account.AccountType == 1 ? x.Account.Brand.BrandName : x.Account.AccountType == 2 ? x.Account.User.Name + " " + x.Account.User.LastName : "Invalid User",
                        Text = x.ReplyText,
                        Date = x.InsertDate
                    })
                }).FirstOrDefault();

            return infoRequest;
        }
    }
}
