using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Entities.Users.Implementations;
using bolsaBE.Models.Users;
using bolsaBE.Models.Users.Student;
using bolsaBE.Services.MailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly BolsaDeTrabajoContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Student> _userManagerStudent;
        private readonly ISystemSupportMail _systemSupportMail;

        public StudentsRepository(BolsaDeTrabajoContext context, IHttpContextAccessor httpContextAccessor, UserManager<Student> userManagerStudent, ISystemSupportMail systemSupportMail)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context; 
            _userManagerStudent = userManagerStudent;
            _systemSupportMail = systemSupportMail;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _context.Students
                .Include(x=>x.DocumentType)
                .Include(x=>x.GenderType)
                .Include(x=>x.CivilStatusType)
                .Include(x=>x.Validation)
                .Include(x => x.OtherData)
                .Include(x => x.Knowledgements)
                .ThenInclude(x => x.KnowledgeType)
                .Include(x => x.Knowledgements)
                .ThenInclude(x =>x.KnowledgeValue)
                .ToList();
        }

        public bool UpdateOtherData(OtherDataToUpdDTO odata)
        {
            var od = new OtherData();
            IFormFile? file = odata.Curriculum;
            byte[]? bytes = null;

            if (file is not null)
            {
                long length = file.Length;

                using var fileStream = file.OpenReadStream();
                bytes = new byte[length];
                fileStream.Read(bytes, 0, (int)file.Length);

                od.FileName = file.FileName;
            }

            od.Curriculum = bytes;
            od.Observations = odata.Observations;
            od.HighSchoolDegree = odata.HighSchoolDegree;

            _context.OtherData.Add(od);
            var save = SaveChange();

            if (!save) return false;

            var studentId = GetRequesterId();

            var student = _userManagerStudent.FindByIdAsync(studentId.ToString()).Result;

            if (student == null) return false;

            student.OtherData = od;
            student.OtherDataId = od.Id;

            var result = _userManagerStudent.UpdateAsync(student).Result;

            return result.Succeeded;

        }

        public OtherData? GetCurrentUserOtherData()
        {
            var data = _context.Students
                .Include(od => od.OtherData)
                .FirstOrDefault(std => std.Id == GetRequesterId());

            return data?.OtherData;
        }

        public UniversityData? GetCurrentUniversityData()
        {
            var universityData = _context.Students
                .Include(ud => ud.UniversityData)
                .Include(ud => ud.UniversityData.Degree)
                .FirstOrDefault(stud => stud.Id == GetRequesterId());

            return universityData?.UniversityData;
        }

        public bool AddNewUniversityData(UniversityData ud)
        {
            _context.UniversityData.Add(ud);
            if (!SaveChange()) 
                return false;
            return true;
        }

        private Guid? GetRequesterId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            if (userId is null) return null;
            return Guid.Parse(userId);
        }
        public bool SaveChange()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task<bool> GenerateForgotPasswordTokenAsync(string email)
        {
            var user = _userManagerStudent.FindByEmailAsync(email).Result;
            var token = await _userManagerStudent.GeneratePasswordResetTokenAsync(user);

            if (!string.IsNullOrEmpty(token))
            {
                string sbj = "Código de recuperación de contraseña - Bolsa de trabajo UTN";
                string body = $"El código de recuperación es {token}";
                _systemSupportMail.SendEmail(sbj, body, "matias.sauan28@gmail.com");
                return true;
            }

            return false;
        }
    }
}
