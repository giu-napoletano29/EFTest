using BusinessAccess.ModelAPI.ProductModels;
using System.Collections;
using System.Collections.Generic;
using TestJuniorDef.ModelAPI;

namespace BusinessAccess.ModelAPI.InfoRequestModels
{
    public class GetInfoRequestByIdModelAPI
    {
        public int Id { get; set; }
        public ProductBasePlusBrandNameModelAPI Product { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Text { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public IEnumerable<ReplyModelAPI> Replies { get; set; }

    }
}
