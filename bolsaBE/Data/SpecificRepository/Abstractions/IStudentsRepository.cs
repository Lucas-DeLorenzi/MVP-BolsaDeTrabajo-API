using bolsaBE.Entities;
using bolsaBE.Entities.Users.Implementations;
using bolsaBE.Models.Users;
using bolsaBE.Models.Users.Student;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IStudentsRepository
    {
        public IEnumerable<Student> GetStudents();
        bool UpdateOtherData(OtherDataToUpdDTO otherData);
        bool AddNewUniversityData(UniversityData ud);
        OtherData? GetCurrentUserOtherData();
        public UniversityData? GetCurrentUniversityData();
    }
}
