using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities.Auxiliary.Types.Implementation
{
    public class KnowledgeType : AuxTable
    {
        public List<Search>? Searches { get; set; }
    }
}
