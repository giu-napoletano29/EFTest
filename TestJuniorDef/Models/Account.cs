using System;
using System.Collections.Generic;

namespace apitest.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Byte[] Password { get; set; }
        public byte AccountType { get; set; }

        public Brand Brand { get; set; }
        public User User { get; set; }
        public ICollection<InfoRequestReply> InfoRequestReply { get; set; }
    }
}
