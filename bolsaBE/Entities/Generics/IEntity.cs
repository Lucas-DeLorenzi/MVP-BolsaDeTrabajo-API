namespace bolsaBE.Entities.Generics
{
    public interface IEntity
    {
        Guid Id { get; set; }
        int Order { get; set; }
    }
}
