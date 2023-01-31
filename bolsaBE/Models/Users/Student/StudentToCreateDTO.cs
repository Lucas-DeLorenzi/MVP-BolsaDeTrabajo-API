using bolsaBE.Entities;
using System.ComponentModel.DataAnnotations;

namespace bolsaBE.Models.Users.Student
{
    public class StudentToCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        public Guid DocumentTypeId { get; set; }
        [Required]
        [MaxLength(10)]
        public string DocumentNumber { get; set; }
        [Required]
        public string FileNumber { get; set; }
        [Required]
        [MaxLength(320)]
        public string Email { get; set; }
        [Required]
        [MaxLength(40)]
        public string Password { get; set; }

    }
}
