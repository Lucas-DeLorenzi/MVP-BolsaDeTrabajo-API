using bolsaBE.Entities.Auxiliary.Types.Implementation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities
{
    public class Knowledge
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("KnowledgeTypeId")]
        public Guid KnowledgeTypeId { get; set; }
        public KnowledgeType KnowledgeType { get; set; }
        [ForeignKey("KnowledgeValueId")]
        public Guid KnowledgeValueId { get; set; }
        public KnowledgeValue KnowledgeValue { get; set; }
    }
}
