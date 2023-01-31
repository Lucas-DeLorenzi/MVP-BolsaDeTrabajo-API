using AutoMapper;
using bolsaBE.Entities;
using bolsaBE.Models.Postulations;

namespace bolsaBE.AutoMapperProfiles
{
    public class PostulationProfile : Profile
    {
        public PostulationProfile()
        {
            CreateMap<Postulation, PostulationDTO>();
        }
    }
}
