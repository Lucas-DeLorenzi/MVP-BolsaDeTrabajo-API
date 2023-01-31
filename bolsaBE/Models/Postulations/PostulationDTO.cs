using bolsaBE.Models.Search;
using bolsaBE.Models.Users.Student;

namespace bolsaBE.Models.Postulations
{
    public class PostulationDTO
    {
        public Guid Id { get; set; }
        public Guid? SearchId { get; set; }
        public SearchInPostulationDTO Search { get; set; }
        public Guid? StudentId { get; set; }
        public StudentInPostulationDTO Student { get; set; }
        public DateTime PostulationDate { get; set; }
    }
}
