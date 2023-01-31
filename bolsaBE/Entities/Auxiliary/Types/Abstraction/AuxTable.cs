using bolsaBE.Entities.Generics;
using System.ComponentModel.DataAnnotations;

namespace bolsaBE.Entities
{
    public abstract class AuxTable : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
