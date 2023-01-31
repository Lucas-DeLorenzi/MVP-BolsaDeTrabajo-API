namespace bolsaBE.Models
{
    public class ContactDTO
    {
        public Guid? RelationTypeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
