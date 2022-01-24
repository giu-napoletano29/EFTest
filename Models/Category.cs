using System.Collections.Generic;

namespace apitest.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
