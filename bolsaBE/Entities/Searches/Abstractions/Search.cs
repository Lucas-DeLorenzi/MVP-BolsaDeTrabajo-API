using bolsaBE.Entities.Auxiliary.Types.Implementation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities
{
    public class Search
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("CompanyId")]
        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }
        
        [MaxLength(100)]
        public string SearchTitle { get; set; }
        
        [MaxLength(1000)]
        public string SearchDescription { get; set; }
        
        public List<Postulation>? Postulations { get; set; }
        
        public string EmailReciever { get; set; }
        
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
        public int? Vacancies { get; set; }
        [ForeignKey("AddressId")]
        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }
        public List<KnowledgeType>? KnowledgeTypes { get; set; }
        public List<Degree>? Degrees { get; set; }
        public string DiscriminatorValue { get; set; } = "Search";
    }
}
