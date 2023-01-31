using bolsaBE.Entities.Generics;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities
{
    public class User : IdentityUser<Guid>
    {
        [ForeignKey("AddressId")]
        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }
        public string? CuilCuit { get; set; }

    }
}
