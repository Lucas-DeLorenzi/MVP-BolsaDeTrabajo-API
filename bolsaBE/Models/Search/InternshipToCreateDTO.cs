using bolsaBE.Entities;
using bolsaBE.Entities.Auxiliary.Types.Implementation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bolsaBE.Models
{
    public class InternshipToCreateDTO
    {
        [Required]
        public int DurationInMonths { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string SearchTitle { get; set; }
        [Required]
        [MaxLength(1000)]
        public string SearchDescription { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        [DefaultValue("2022-10-22T23:52:24.827Z")]
        public DateTime DateUntil { get; set; }
        [Required]
        public int Vacancies { get; set; }
        public List<string> DegreesId { get; set; }
        public List<string>? KnowledgementTypeId { get; set; }
    }
}
