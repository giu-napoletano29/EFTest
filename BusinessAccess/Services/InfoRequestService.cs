using apitest.Models;
using BusinessAccess.ModelAPI.InfoRequestModels;
using BusinessAccess.ModelAPI.ProductModels;
using BusinessAccess.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.Repositories.Interfaces;

namespace BusinessAccess.Services
{
    public class InfoRequestService : IInfoRequestService
    {
        private readonly IInfoRequestRepo _inforeqrepo;
        public InfoRequestService(IInfoRequestRepo inforeqrepo)
        {
            _inforeqrepo = inforeqrepo;
        }

        /// <summary>
        /// Return a collection with all the InfoRequests present in the database 
        /// </summary>
        /// <returns><see cref="IEnumerable{T}">List of brands</see></returns>
        public IEnumerable<InfoRequest> GetInfoRequests()
        {
            return _inforeqrepo.GetAll(false);
        }

        /// <summary>
        /// Return informations about an InfoRequest by a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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
                    Text = x.RequestText,
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

        /// <summary>
        /// Return a page of Inforequests with a specified lenght and search filters.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="brand"></param>
        /// <param name="search"></param>
        /// <param name="productId"></param>
        /// <param name="orderdesc"></param>
        /// <returns></returns>
        public PagingModelAPI<InfoRequestPagingModelAPI> GetInfoRequestsPerPage(int size = 5, int page = 1, int brand = 0, string search = "", int productId = 0, bool orderdesc = false)
        {
            var repo = _inforeqrepo.GetAll(true);
            if (!orderdesc)
            {
                repo = repo.OrderByDescending(x => x.InsertDate);
            }
            else
            {
                repo = repo.OrderBy(x => x.InsertDate);
            }
            if (productId > 0)
            {
                repo = repo.Where(x => x.ProductId == productId);
            }
            if (brand > 0)
            {
                repo = repo.Where(x => x.Product.BrandId == brand);
            }
            if (!string.IsNullOrWhiteSpace(search) && search.Length > 0)
            {
                search = search.TrimStart();
                repo = repo.Where(x => x.Product.Name.Contains(search));
            }
            var pagination = Service.PaginateEntity(repo, size, page);
            PagingModelAPI<InfoRequestPagingModelAPI> model = new PagingModelAPI<InfoRequestPagingModelAPI>();
            model.PageSize = pagination.PageSize;
            model.TotalElements = pagination.TotalElements;
            model.NumPage = pagination.NumPage;
            model.Elements = pagination.Elements.Select(x => new InfoRequestPagingModelAPI
            {
                Id = x.Id,
                Name = x.Name,
                Lastname = x.LastName,
                Text = x.RequestText,
                InsertDate = x.InsertDate,
                productId = x.ProductId,
                productName = x.Product.Name,
                brandId = x.Product.BrandId,
                brandName = x.Product.Brand.BrandName
            }).ToList();

            return model;
        }
    }
}
