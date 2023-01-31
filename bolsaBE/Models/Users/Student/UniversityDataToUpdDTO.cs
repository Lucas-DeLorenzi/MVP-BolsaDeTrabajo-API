using bolsaBE.Enums;
using System.Text.Json.Serialization;

namespace bolsaBE.Models.Users.Student
{
    public class UniversityDataToUpdDTO
    {
        public Guid? DegreeId { get; set; }
        public string? ApprovedSubjects { get; set; }
        public string? DegreePlanYear { get; set; }
        public string? CurrentCourseYear { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CourseShift CourseShift { get; set; }
        public string? AverageWithHeldBacks { get; set; }
        public string? AverageWithoutHeldBacks { get; set; }
    }
}
