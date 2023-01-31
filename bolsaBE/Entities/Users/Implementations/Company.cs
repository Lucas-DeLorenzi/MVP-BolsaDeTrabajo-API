using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities
{
    public class Company : User
    {
        [Required]
        [MaxLength(50)]
        public string BusinessName { get; set; }
        public string Name { get => BusinessName; }

        [MaxLength(50)]
        public string? Sector { get; set; }
        public bool? FrameworkAgreement { get; set; }
        [MaxLength(50)]

        [ForeignKey("ValidationId")]
        public Guid ValidationId { get; set; }
        public Validation Validation { get; set; }

        [ForeignKey("ContactId")]
        public Guid? ContactId { get; set; }
        public Contact? Contact { get; set; }
        public string? WebSite { get; set; }

        public Company(string businessName)
        {
            var validation = new Validation();
            Validation = validation;
            ValidationId = validation.Id;
            BusinessName = businessName;
        }

        //public Company() { }

    }
}
