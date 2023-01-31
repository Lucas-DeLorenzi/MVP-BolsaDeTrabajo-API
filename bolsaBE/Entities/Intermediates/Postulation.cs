using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities
{
    public class Postulation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("SearchId")]
        public Guid? SearchId { get; set; }
        public Search Search { get; set; }
        [ForeignKey("StudentId")]
        public Guid? StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime PostulationDate { get; set; } = DateTime.Now;
    }
}
