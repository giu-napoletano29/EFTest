﻿using System.Collections.Generic;

namespace apitest.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }


        public Account Account { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
