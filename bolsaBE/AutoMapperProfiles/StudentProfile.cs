using AutoMapper;
using bolsaBE.Entities;
using bolsaBE.Entities.Users.Implementations;
using bolsaBE.Models.Users;
using bolsaBE.Models.Users.Student;

namespace bolsaBE.AutoMapperProfiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>().ForMember(x => x.Birthday, opt => opt.Ignore()); ;
            CreateMap<Student, StudentInPostulationDTO>();
            CreateMap<Student, StudentToCreateDTO>();
            CreateMap<StudentDTO, Student>();
            CreateMap<StudentToCreateDTO, Student>();
            CreateMap<OtherDataToUpdDTO, OtherData>();
            CreateMap<OtherData, OtherDataToUpdDTO>();
            CreateMap<OtherDataDTO, OtherData>();
            CreateMap<OtherData, OtherDataDTO>();
            CreateMap<UniversityDataDTO, UniversityData>();
            CreateMap<UniversityData, UniversityDataDTO>();
            CreateMap<UniversityDataToUpdDTO, UniversityData>();
            CreateMap<UniversityData, UniversityDataToUpdDTO>();
            CreateMap<StudentPersonalDataDTO, Student>().ForMember(x => x.Birthday, opt => opt.Ignore());
            CreateMap<Student, StudentPersonalDataDTO>().ForMember(x => x.Birthday, opt => opt.Ignore());
        }
    }
}
