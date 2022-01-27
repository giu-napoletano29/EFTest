using System;
using System.Collections.Generic;

namespace apitest.Models
{
    public class InfoRequest
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public int NationId { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string RequestText { get; set; }
        public DateTime InsertDate { get; set; }


        public User User { get; set; }
        public Product Product { get; set; }
        public Nation Nation { get; set; }
        public ICollection<InfoRequestReply> InfoRequestReply { get; set; }

    }
}
