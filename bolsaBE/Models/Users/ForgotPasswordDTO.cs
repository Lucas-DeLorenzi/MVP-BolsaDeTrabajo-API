using System.ComponentModel.DataAnnotations;

namespace bolsaBE.Models.Users
{
    public class ForgotPasswordDTO
    {

        [Required]

        public string Email { get; set; }

        public string Password { get; set; }

        public string Code { get; set; }
    }
}
