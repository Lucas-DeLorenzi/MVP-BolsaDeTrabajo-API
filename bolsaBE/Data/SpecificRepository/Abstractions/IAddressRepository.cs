using bolsaBE.DBContexts;
using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IAddressRepository
    {
        public Address GetAddressById(Guid? id);
        public bool AddAddress(Address address);
    }
}
