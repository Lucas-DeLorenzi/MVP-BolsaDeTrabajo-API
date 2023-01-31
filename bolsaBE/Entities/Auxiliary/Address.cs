using System.ComponentModel.DataAnnotations;

namespace bolsaBE.Entities
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? LetterBis { get; set; }
        public string? Floor { get; set; }
        public string? Apartment { get; set; }
        public string? PostalCode { get; set; }
        // por ahora usamos un string para ciudad, reemplazar por una Localidad
        public string? City { get; set; }
    }
}
