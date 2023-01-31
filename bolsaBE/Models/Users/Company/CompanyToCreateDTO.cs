using System.ComponentModel.DataAnnotations;

namespace bolsaBE.Models.Users.Company
{
    public class CompanyToCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string BusinessName { get; set; }
        [Required]
        [MaxLength(10)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Ingrese un mail válido")]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
