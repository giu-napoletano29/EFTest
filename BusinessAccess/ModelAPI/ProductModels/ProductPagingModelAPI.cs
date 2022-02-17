using BusinessAccess.ModelAPI.CategoryModels;
using System.Collections.Generic;

namespace BusinessAccess.ModelAPI.ProductModels
{
    public class ProductPagingModelAPI : ProductBaseModelAPI
    {
        public string BrandName { get; set; }
        public string ImageURL { get; set; } = "https://via.placeholder.com/300";
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<CategoryBaseModelAPI> Categories { get; set; }

    }
}
