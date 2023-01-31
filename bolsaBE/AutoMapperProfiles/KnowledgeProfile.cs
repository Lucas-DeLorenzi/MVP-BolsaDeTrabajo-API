using AutoMapper;
using bolsaBE.Entities;
using bolsaBE.Entities.Auxiliary.Types.Implementation;
using bolsaBE.Models;

namespace bolsaBE.AutoMapperProfiles
{
    public class KnowledgeProfile : Profile
    {
        public KnowledgeProfile()
        {
            CreateMap<Knowledge, KnowledgeDTO>();
            CreateMap<KnowledgeDTO, Knowledge>();
            CreateMap<KnowledgeType, KnowledgeTypeToCreateDTO>();
            CreateMap<KnowledgeTypeToCreateDTO, KnowledgeType>();
        }
    }
}
