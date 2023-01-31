using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IContactRepository
    {
        public Contact GetContactById(Guid? id);
        public bool AddContact(Contact contact);
    }
}
