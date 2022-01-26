using System;

namespace TestJuniorDef.ModelAPI.InfoRequestModels
{
    public class InfoRequestModelAPI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int TotalReply { get; set; }
        public DateTime DateLastReply { get; set; }
    }
}
