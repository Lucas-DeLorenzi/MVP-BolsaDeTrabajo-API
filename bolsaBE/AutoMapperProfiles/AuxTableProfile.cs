using AutoMapper;
using bolsaBE.Entities;
using bolsaBE.Models;

namespace bolsaBE.AutoMapperProfiles
{
    public class AuxTableProfile : Profile
    {
        public AuxTableProfile()
        {
            CreateMap<AuxTable, AuxTableDTO>().IncludeAllDerived();
            CreateMap<AuxTableDTO, AuxTable>().IncludeAllDerived();
        }
    }
}
