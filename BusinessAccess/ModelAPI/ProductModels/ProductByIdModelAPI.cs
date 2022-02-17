using BusinessAccess.ModelAPI.CategoryModels;
using BusinessAccess.ModelAPI.InfoRequestModels;
using System.Collections.Generic;

namespace BusinessAccess.ModelAPI.ProductModels
{
    public class ProductByIdModelAPI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<CategoryBaseModelAPI> Categories { get; set; }
        public int TotalInfoRequestGuest { get; set; }
        public int TotalInfoRequestLogged { get; set; }
        public IEnumerable<InfoRequestModelAPI> InfoRequest { get; set; }


    }
}
