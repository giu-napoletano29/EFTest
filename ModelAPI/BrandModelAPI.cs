using System.Collections.Generic;

namespace TestJuniorDef.ModelAPI
{
    public class BrandModelAPI
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int TotalProducts { get; set; }
        public int TotalInfoRequest { get; set; }
        public IEnumerable<CategoryModelAPI> Categories { get; set; }
        public IEnumerable<ProductModelAPI> Products { get; set; }
    }
}
