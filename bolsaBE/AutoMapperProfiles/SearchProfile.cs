using AutoMapper;
using bolsaBE.Entities;
using bolsaBE.Entities.Auxiliary.Types.Implementation;
using bolsaBE.Models;
using bolsaBE.Models.Search;

namespace bolsaBE.AutoMapperProfiles
{
    public class SearchProfile : Profile
    {
        public SearchProfile()
        {
            CreateMap<Search, JobDTO>();
            CreateMap<Search, InternshipDTO>();

            CreateMap<SearchDTO, Search>();
            CreateMap<Search, SearchDTO>();
            CreateMap<SearchInPostulationDTO, Search>();
            CreateMap<Search, SearchInPostulationDTO>();

            CreateMap<Internship, InternshipDTO>();
            CreateMap<InternshipDTO, Internship>();
            CreateMap<Internship, InternshipToCreateDTO>();
            CreateMap<InternshipToCreateDTO, Internship>();
            CreateMap<Internship, InternshipToUpdateDTO>();
            CreateMap<InternshipToUpdateDTO, Internship>();

            CreateMap<Job, JobDTO>();
            CreateMap<JobDTO, Job> ();
            CreateMap<Job, JobToCreateDTO>();
            CreateMap<JobToCreateDTO, Job>();
            CreateMap<Job, JobToUpdateDTO>();
            CreateMap<JobToUpdateDTO, Job>();

            CreateMap<KnowledgeType, KnowledgeTypesDTO>();
        }
    }
}
