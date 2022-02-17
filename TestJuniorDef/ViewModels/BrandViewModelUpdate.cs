using apitest.Models;
using System.Collections.Generic;

namespace TestJuniorDef.ViewModels
{
    public class BrandViewModelUpdate
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }


        public AccountViewModelUpdate Account { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
