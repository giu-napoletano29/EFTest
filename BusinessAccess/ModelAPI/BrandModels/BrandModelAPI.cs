using BusinessAccess.ModelAPI.CategoryModels;
using BusinessAccess.ModelAPI.ProductModels;
using System;
using System.Collections.Generic;

namespace BusinessAccess.ModelAPI.BrandModels
{
    public class BrandModelAPI
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string Description { get; set; }
        public int TotalProducts { get; set; }
        public int TotalInfoRequest { get; set; }
        public IEnumerable<CategoryModelAPI> Categories { get; set; }
        public IEnumerable<ProductModelAPI> Products { get; set; }
    }
}
