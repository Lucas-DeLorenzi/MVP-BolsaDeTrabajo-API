using bolsaBE.Models.Users;
using bolsaBE.Models.Users.Student;

namespace bolsaBE.Services.Abstractions
{
    public interface IStudentServices
    {
        IEnumerable<StudentDTO> GetStudents();
        StudentPersonalDataDTO? GetCurrentPersonalData();
        StudentDTO? StudentSignUp(StudentToCreateDTO student);
        IEnumerable<StudentToValidateDTO> GetStudentsToValidate();
        string PutUpdatedAtForStudent(string id);
        bool StudentUpdateOtherData(OtherDataToUpdDTO otherData);
        public bool StudentUpdateUniversityData(UniversityDataToUpdDTO universityData);
        OtherDataDTO? GetCurrentOtherData();
        UniversityDataDTO? GetCurrentUniversityData();
        bool UpdateStudentPersonalData(StudentPersonalDataDTO studentPersonalData);
        bool AnyRequiredFieldIsNull(StudentPersonalDataDTO studentPersonalData);
        bool CheckUniqueFields(string field, string value);
        bool RemoveStudent(Guid studentId);
    }
}
