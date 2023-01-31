using System.ComponentModel.DataAnnotations.Schema;

namespace bolsaBE.Entities
{
    public class Internship : Search
    {
        public int DurationInMonths { get; set; }
        public DateTime StartDate { get; set; }

        [ForeignKey("ValidationId")]
        public Guid ValidationId { get; set; }
        public Validation Validation { get; set; }


        public Internship()
        {
            var validation = new Validation();
            Validation = validation;
            ValidationId = validation.Id;
            DiscriminatorValue = "Pasantía a Verificar";
        }
    }
}
