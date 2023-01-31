using AutoMapper;
using bolsaBE.Entities;
using bolsaBE.Models;

namespace bolsaBE.AutoMapperProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();
        }
    }
}
