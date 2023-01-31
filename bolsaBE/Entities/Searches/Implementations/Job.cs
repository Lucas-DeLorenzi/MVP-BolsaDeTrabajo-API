using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities
{
    public class Job : Search
    {
        [ForeignKey("WorkdayTypeId")]
        public Guid WorkdayTypeId { get; set; }
        public WorkdayType WorkdayType { get; set; }

        public Job()
        {
            DiscriminatorValue = "Empleo";
        }
    }
}
