using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly BolsaDeTrabajoContext _context;
        public AddressRepository(BolsaDeTrabajoContext context)
        {
            _context = context;
        }

        public Address GetAddressById(Guid? id)
        {
            return _context.Addresses.Find(id);
        }

        public bool AddAddress(Address address)
        {
            _context.Addresses.Add(address);
            return (_context.SaveChanges() >= 0);
        }
    }
}
