using bolsaBE.Enums;
using System.Text.Json.Serialization;

namespace bolsaBE.Models
{
    public class DegreeDTO
    {
        public string? DegreeId { get; set; }
        public string DegreeTitle { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DegreeCategory DegreeCategory { get; set; }
        public int TotalSubjects { get; set; }
    }
}
