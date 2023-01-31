namespace bolsaBE.Models.Users.Company
{
    public class CompanyDataDTO
    {
        public string BusinessName { get; set; }
        public string CuilCuit { get; set; }
        public string Email { get; set; }
        public string Sector { get; set; }
        public AddressDTO Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? WebSite { get; set; }
        public ContactDTO Contact { get; set; }
    }
}
