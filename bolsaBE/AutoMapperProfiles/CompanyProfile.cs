using AutoMapper;
using bolsaBE.Entities;
using bolsaBE.Models.Users.Company;

namespace bolsaBE.AutoMapperProfiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDTO>();
            CreateMap<Company, CompanyToCreateDTO>();
            CreateMap<Company, CompanyDataDTO>();
            CreateMap<Company, CompanyUniqueFieldsDTO>();
            CreateMap<CompanyDTO, Company>();
            CreateMap<CompanyToCreateDTO, Company>();
            CreateMap<CompanyDataDTO, Company>();
            CreateMap<CompanyUniqueFieldsDTO, Company>();
        }
    }
}
