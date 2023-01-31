using System.ComponentModel.DataAnnotations;

namespace bolsaBE.Entities.Users.Implementations
{
    public class OtherData
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Observations { get; set; }
        public string? HighSchoolDegree { get; set; }
        public byte[]? Curriculum { get; set; }
        public string? FileName { get; set; }

    }
}
