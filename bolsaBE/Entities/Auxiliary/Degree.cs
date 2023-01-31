using bolsaBE.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities
{
    public class Degree
    {
        [Key]
        public Guid DegreeId { get; set; }
        [Required]
        [MaxLength(40)]
        public string DegreeTitle { get; set; } = string.Empty;
        [Required]
        [MaxLength (10)]
        public string Abbreviation { get; set; } = string.Empty;
        [Required]
        [MaxLength(16)]
        public DegreeCategory DegreeCategory { get; set; }
        [Required]
        [MaxLength (60)]
        public int TotalSubjects { get; set; }
        public List<Search>? Search { get; set; }
    }
}
