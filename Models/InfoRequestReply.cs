using System;

namespace apitest.Models
{
    public class InfoRequestReply
    {
        public int Id { get; set; }
        public int InfoRequestId { get; set; }
        public int AccountId { get; set; }
        public string ReplyText { get; set; }
        public DateTime InsertDate { get; set; }

        public InfoRequest InfoRequest { get; set; }
        public Account Account { get; set; }
    }
}
