using System.Collections.Generic;

namespace apitest.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }


        public Brand Brand { get; set; }
        public ICollection<InfoRequest> InfoRequests { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
