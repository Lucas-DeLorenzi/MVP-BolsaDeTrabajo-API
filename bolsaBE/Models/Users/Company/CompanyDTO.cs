using bolsaBE.Entities;

namespace bolsaBE.Models.Users.Company
{
    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public string Name { get => BusinessName; }
        public string CuilCuit { get; set; }
        public string Email { get; set; }
        public string? Sector { get; set; }
        public string PhoneNumber { get; set; }
        public Address? Address { get; set; }
        public Contact? Contact { get; set; }
        public Validation? Validation { get; set; }

    }
}
