using AutoMapper;

namespace bolsaBE.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, Models.UserDTO>();
            CreateMap<Entities.User, Entities.Company>();
        }
    }
}
