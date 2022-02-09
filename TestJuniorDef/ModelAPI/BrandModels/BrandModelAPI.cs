using System;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI.CategoryModels;
using TestJuniorDef.ModelAPI.ProductModels;

namespace TestJuniorDef.ModelAPI
{
    public class BrandModelAPI
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Email { get; set; }
        public Byte[] Password { get; set; }
        public string Description { get; set; }
        public int TotalProducts { get; set; }
        public int TotalInfoRequest { get; set; }
        public IEnumerable<CategoryModelAPI> Categories { get; set; }
        public IEnumerable<ProductModelAPI> Products { get; set; }
    }
}
