using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("RelationTypeId")]
        public Guid? RelationTypeId { get; set; }
        public  RelationType? RelationType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Name { get => FirstName + " " + LastName; }
        public string? Position { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
