using System;

namespace TestJuniorDef.ModelAPI.InfoRequestModels
{
    public class InfoRequestPagingModelAPI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Text { get; set; }
        public DateTime InsertDate { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public int brandId { get; set; }
        public string brandName { get; set; }
    }
}
