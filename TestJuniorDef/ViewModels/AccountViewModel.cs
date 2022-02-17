namespace TestJuniorDef.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte AccountType { get; set; }
        public bool IsDeleted { get; set; }
    }
}
