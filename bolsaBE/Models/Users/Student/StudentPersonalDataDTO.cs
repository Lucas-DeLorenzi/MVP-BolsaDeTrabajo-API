using bolsaBE.Entities;

namespace bolsaBE.Models.Users.Student
{
    public class StudentPersonalDataDTO
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public string? DocumentNumber { get; set; }
        public string? FileNumber { get; set; }
        public string? Email { get; set; }
        public string? CuilCuit { get; set; }
        public DateTime Birthday { get; set; }
        public Guid? GenderTypeId { get; set; }
        public Guid? CivilStatusTypeId { get; set; }
        public AddressDTO? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
