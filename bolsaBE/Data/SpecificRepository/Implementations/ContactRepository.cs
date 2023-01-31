using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Implementations
{

    public class ContactRepository : IContactRepository
    {
        private readonly BolsaDeTrabajoContext _context;
        public ContactRepository(BolsaDeTrabajoContext context)
        {
            _context = context;
        }
        public Contact GetContactById(Guid? id)
        {
           return _context.Contacts.Find(id);
        }

        public bool AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            return (_context.SaveChanges() >= 0);
        }
    }

}
