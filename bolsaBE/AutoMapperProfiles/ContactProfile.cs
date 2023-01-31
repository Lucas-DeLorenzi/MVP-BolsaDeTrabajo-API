using AutoMapper;
using bolsaBE.Entities;
using bolsaBE.Models;

namespace bolsaBE.AutoMapperProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactDTO, Contact>();
        }
    }
}
