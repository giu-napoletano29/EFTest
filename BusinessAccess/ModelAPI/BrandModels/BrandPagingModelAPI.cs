using System.Collections.Generic;

namespace BusinessAccess.ModelAPI.BrandModels
{
    public class BrandPagingModelAPI
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }

        public List<int> ProductsId { get; set; }
    }
}
