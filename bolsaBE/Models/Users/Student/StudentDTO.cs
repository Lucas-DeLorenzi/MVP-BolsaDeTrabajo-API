using bolsaBE.Entities;
using bolsaBE.Entities.Users.Implementations;

namespace bolsaBE.Models.Users.Student
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Name { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public AuxTableDTO DocumentType { get; set; }
        public string? DocumentNumber { get; set; }
        public string? FileNumber { get; set; }
        public string? Email { get; set; }
        public string? CuilCuit { get; set; }
        public DateTime? Birthday { get; set; }
        public Guid? GenderTypeId { get; set; }
        public Guid? CivilStatusTypeId { get; set; }
        public Guid? ValidationId { get; set; }
        public Validation? Validation { get; set; }
        public Guid? OtherDataId { get; set; }
        public OtherData? OtherData { get; set; }
        public Address address { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Knowledge>? Knowledgements { get; set; }
        public List<Postulation>? Postulations { get; set; }

    }
}
