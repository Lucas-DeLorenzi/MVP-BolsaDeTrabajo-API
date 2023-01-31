using bolsaBE.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bolsaBE.Models
{
    public class JobToUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid WorkdayTypeId { get; set; }

        [Required]
        public Guid CompanyId { get; set; }
        [Required]
        [MaxLength(100)]
        public string SearchTitle { get; set; }

        [Required]
        [MaxLength(1000)]
        public string SearchDescription { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Debe ingresar un email válido")]
        public string EmailReciever { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateUntil { get; set; }
        [Required]
        public int Vacancies { get; set; }

    }
}
