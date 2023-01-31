using bolsaBE.Entities.Generics;
using bolsaBE.Entities.Users.Implementations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace bolsaBE.Entities
{
    public class Student : User
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Name { get => FirstName + " " + LastName; }

        [ForeignKey("DocumentTypeId")]
        public Guid? DocumentTypeId { get; set; }
        public DocumentType? DocumentType { get; set; }

        public string? DocumentNumber { get; set; }

        public string? FileNumber { get; set; }
        public DateOnly Birthday { get; set; }

        [ForeignKey("GenderTypeId")]
        public Guid? GenderTypeId { get; set; }
        public GenderType? GenderType { get; set; }

        [ForeignKey("CivilStatusTypeId")]
        public Guid? CivilStatusTypeId { get; set; }
        public CivilStatusType? CivilStatusType { get; set; }
        [ForeignKey("UniversityDataId")]
        public Guid? UniversityDataId { get; set; }
        public UniversityData? UniversityData { get; set; }


        [ForeignKey("OtherDataId")]
        public Guid? OtherDataId { get; set; }
        public OtherData? OtherData { get; set; }

        [ForeignKey("ValidationId")]
        public Guid ValidationId { get; set; }
        public Validation Validation { get; set; }

        public List<Knowledge>? Knowledgements { get; set; }
        public List<Postulation>? Postulations { get; set; }

        public Student(string firstName, string lastName)
        {
            var validation = new Validation();
            Validation = validation;
            ValidationId = validation.Id;
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
