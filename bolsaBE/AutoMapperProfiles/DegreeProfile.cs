using AutoMapper;

namespace bolsaBE.AutoMapperProfiles
{
    public class DegreeProfile : Profile
    {
        public DegreeProfile()
        {
            CreateMap<Entities.Degree, Models.DegreeDTO>();
            CreateMap<Models.DegreeDTO, Entities.Degree>();
            CreateMap<Entities.Degree, Models.DegreeToCreateDTO>();
            CreateMap<Models.DegreeToCreateDTO, Entities.Degree>();
            CreateMap<Entities.Degree, Models.DegreeToUpdDTO>();
            CreateMap<Models.DegreeToUpdDTO, Entities.Degree>();
        }
    }
}
