using bolsaBE.Entities;
using bolsaBE.Entities.Auxiliary.Types.Implementation;

namespace bolsaBE.Models
{
    public class KnowledgeDTO
    {
        public Guid Id { get; set; }       
        public Guid KnowledgeTypeId { get; set; }
        public AuxTableDTO KnowledgeType { get; set; }
        public Guid KnowledgeValueId { get; set; }
        public AuxTableDTO KnowledgeValue { get; set; }

    }
}
