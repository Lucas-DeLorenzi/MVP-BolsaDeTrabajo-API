using bolsaBE.Entities;

namespace bolsaBE.Models.Users.Student
{
    public class StudentInPostulationDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public DocumentType? DocumentType { get; set; }
        public int DocumentNumber { get; set; }
        public string? FileNumber { get; set; }
        public string? Email { get; set; }
        public string? CuilCuit { get; set; }
        public DateOnly? Birthday { get; set; }
        public Guid? GenderTypeId { get; set; }
        public GenderType? GenderType { get; set; }
        public Guid? CivilStatusTypeId { get; set; }
        public CivilStatusType? CivilStatusType { get; set; }
        public List<Knowledge>? Knowledgements { get; set; }
        public Guid? OtherDataId { get; set; }
        public OtherDataDTO? OtherData { get; set; }
    }
}
