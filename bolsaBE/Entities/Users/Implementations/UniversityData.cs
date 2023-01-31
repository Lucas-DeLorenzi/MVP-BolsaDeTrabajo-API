using bolsaBE.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities.Users.Implementations
{
    public class UniversityData
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("DegreeId")]
        public Guid? DegreeId { get; set; }
        public Degree? Degree { get; set; }
        public string? ApprovedSubjects { get; set; }
        public string? DegreePlanYear { get; set; }
        public string? CurrentCourseYear { get; set; }
        public CourseShift CourseShift { get; set; }
        public string? AverageWithHeldBacks { get; set; }
        public string? AverageWithoutHeldBacks { get; set; }
    }
}
