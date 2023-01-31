using bolsaBE.Entities;
using bolsaBE.Entities.Auxiliary.Types.Implementation;
using bolsaBE.Models.Postulations;
using bolsaBE.Models.Users.Company;

namespace bolsaBE.Models.Search
{
    public class InternshipDTO
    {
        public Guid Id { get; set; }
        public Guid? CompanyId { get; set; }
        public CompanyDTO? Company { get; set; }

        public string SearchTitle { get; set; }

        public string SearchDescription { get; set; }

        public List<PostulationDTO>? Postulations { get; set; }

        public string EmailReciever { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
        public int? Vacancies { get; set; }
        public Address? Address { get; set; }
        public List<KnowledgeTypesDTO>? KnowledgeTypes { get; set; }
        public List<Degree>? Degrees { get; set; }
        public int DurationInMonths { get; set; }
        public DateTime StartDate { get; set; }
        public Guid ValidationId { get; set; }
        public Validation Validation { get; set; }
        public string DiscriminatorValue { get; set; }
    }
}
