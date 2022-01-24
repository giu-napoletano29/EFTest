using System.Collections.Generic;

namespace apitest.Models
{
    public class User
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }


        public Account Account { get; set; }
        public ICollection<InfoRequest> InfoRequests { get; set; }

    }
}
