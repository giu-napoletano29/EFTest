using System;

namespace TestJuniorDef.ViewModels
{
    public class AccountViewModelUpdate
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Byte[] Password { get; set; }
        public byte AccountType { get; set; }
        public bool IsDeleted { get; set; }
    }
}
