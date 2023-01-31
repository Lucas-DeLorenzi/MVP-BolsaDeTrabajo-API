using AutoMapper;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Entities.Users.Implementations;
using bolsaBE.Models.Users;
using bolsaBE.Models.Users.Student;
using bolsaBE.Services.Abstractions;
using bolsaBE.Services.MailServices;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace bolsaBE.Services.Implementations
{
    public class StudentServices : IStudentServices
    {
        private readonly UserManager<User> _userManager;
        private readonly UserManager<Student> _userManagerStudent;
        private readonly UserManager<Admin> _userManagerAdmin;
        private readonly IDegreesRepository _degreesRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly IValidationRepository _validationRepository;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ISystemSupportMail _systemSupportMail;
        private readonly BolsaDeTrabajoContext _context;

        public StudentServices(IMapper mapper,
            UserManager<User> userManager,
            IDegreesRepository degreesRepository,
            IAddressRepository addressRepository,
            IStudentsRepository studentsRepository,
            IHttpContextAccessor httpContextAccessor,
            IValidationRepository validationRepository,
            UserManager<Student> userManagerStudent,
            UserManager<Admin> userManagerAdmin,
            ISystemSupportMail systemSupportMail,
            BolsaDeTrabajoContext context
            )
        {
            _userManager = userManager;
            _userManagerStudent = userManagerStudent;
            _userManagerAdmin = userManagerAdmin;
            _degreesRepository = degreesRepository;
            _addressRepository = addressRepository;
            _studentsRepository = studentsRepository;
            _httpContextAccessor = httpContextAccessor;
            _validationRepository = validationRepository;
            _mapper = mapper;
            _systemSupportMail = systemSupportMail;
            _context = context;
        }
        public IEnumerable<StudentDTO> GetStudents()
        {
            return _mapper.Map<IEnumerable<StudentDTO>>(_studentsRepository.GetStudents());
        }

        public StudentPersonalDataDTO? GetCurrentPersonalData()
        {
            var student = _userManagerStudent.FindByIdAsync(GetCurrentUserId()).Result;
            student.Address = _addressRepository.GetAddressById(student.AddressId);
            var currentStudent = _mapper.Map<StudentPersonalDataDTO>(student);
            currentStudent.Birthday = student.Birthday.ToDateTime(TimeOnly.Parse("10:00 PM")).Date;
            return currentStudent;
        }

        public OtherDataDTO? GetCurrentOtherData()
        {
            return _mapper.Map<OtherDataDTO>(_studentsRepository.GetCurrentUserOtherData());
        }

        public UniversityDataDTO? GetCurrentUniversityData()
        {
            return _mapper.Map<UniversityDataDTO>(_studentsRepository.GetCurrentUniversityData());
        }

        public bool CheckUniqueFields(string field, string value)
        {
            var users = _studentsRepository.GetStudents();
            switch (field)
            {
                case "email":
                    return users.Where(u => u.Email == value).Any();
                case "userName":
                    return users.Where(u => u.UserName == value).Any();
                case "fileNumber":
                    return users.Where(u => u.FileNumber == value).Any();
                case "documentNumber":
                    return users.Where(u => u.DocumentNumber == value).Any();
                case "cuilCuit":
                    return users.Where(u => u.CuilCuit == value).Any();
                default:
                    return false;
            }
        }
        public StudentDTO? StudentSignUp(StudentToCreateDTO student)
        {
            Student studentToCreate = new Student(student.FirstName, student.LastName)
            {
                DocumentTypeId = student.DocumentTypeId,
                DocumentNumber = student.DocumentNumber,
                FileNumber = student.FileNumber,
                Email = student.Email,
                UserName = student.Email,
            };
            var newUser =  _userManager.CreateAsync(studentToCreate, student.Password).Result;
            var addRole = _userManager.AddToRoleAsync(studentToCreate, "Alumno").Result;
            if (newUser.Succeeded && addRole.Succeeded)
            {
                string sbj = "Bienvenido a la Bolsa de Trabajo de la UTN";

                string body = MailBodyHelper.MailGenericWelcomeBody($"{student.FirstName} {student.LastName}");

                _systemSupportMail.SendEmail(sbj, body, student.Email);

                var studentCreated =  _userManager.FindByIdAsync(studentToCreate.Id.ToString()).Result;
                return _mapper.Map<StudentDTO>(studentCreated);
            }
            return null;
        }

        public IEnumerable<StudentToValidateDTO> GetStudentsToValidate()
        {
            return _studentsRepository.GetStudents().Where(s =>
                    s.FirstName != null && s.LastName != null && s.UserName != null &&
                    s.FileNumber != null && s.Email != null && s.DocumentType != null &&
                    s.DocumentNumber != null && s.GenderType != null && s.CivilStatusType != null &&
                    s.Validation.UpdatedAt is null)
                    .Select(s => new StudentToValidateDTO { Name = s.Name, Id = s.Id, Role = "ALUMNO", Path = "student" }).ToList();
        }

        public string PutUpdatedAtForStudent(string id)
        {
            Student? student = null;
            try
            {
                student = _userManagerStudent.FindByIdAsync(id).Result;
            }
            catch(Exception e)
            {
                return e.Message;
            }

            if (student is null)
                return "Not Found";
            var admin = _userManagerAdmin.FindByIdAsync(GetCurrentUserId()).Result;
            student.Validation = _validationRepository.GetValidationById(student.ValidationId);
            if (student.Validation.UpdatedAt != null)
                return "El usuario ya ha sido validado";
            student.Validation.UpdatedAt = DateTime.Now;
            student.Validation.AdminId = admin.Id;
            student.Validation.Responsible = admin;
            var validatedStudent = _userManager.UpdateAsync(student).Result;
            if (validatedStudent.Succeeded)
            {
                string sbj = "Has sido validado en la Bolsa de Trabajo de la UTN";
                string body = MailBodyHelper.MailGenericValidateBody($"{student.FirstName} {student.LastName}");

                _systemSupportMail.SendEmail(sbj, body, student.Email);
                return "Ok";
            }
            return "Error";
        }
        public bool StudentUpdateOtherData(OtherDataToUpdDTO otherData)
        {
            return _studentsRepository.UpdateOtherData(otherData);
        }

        public bool UpdateStudentPersonalData(StudentPersonalDataDTO studentPersonalData)
        {
            var currentStudent = _userManagerStudent.FindByIdAsync(GetCurrentUserId()).Result;
            if (currentStudent.AddressId != null)
            {
                currentStudent.Address = _addressRepository.GetAddressById(currentStudent.AddressId);
            }
            else
            {
                var address = new Address();
                currentStudent.Address = address;
                currentStudent.AddressId = address.Id;
                if(!_addressRepository.AddAddress(address))
                    return false;
                
            }
            currentStudent.Birthday = DateOnly.FromDateTime(studentPersonalData.Birthday);
            _mapper.Map(studentPersonalData.Address, currentStudent.Address);
            
            var student = _mapper.Map(studentPersonalData, currentStudent);
            var result = _userManager.UpdateAsync(student).Result;
            return result.Succeeded;
        }

        public bool StudentUpdateUniversityData(UniversityDataToUpdDTO universityData)
        {
            var currentStudent = _userManagerStudent.FindByIdAsync(GetCurrentUserId()).Result;
            
            if(currentStudent.UniversityDataId != null)
            {
                currentStudent.UniversityData = _studentsRepository.GetCurrentUniversityData();
                
            }
            else
            {
                var ud = new UniversityData();
                currentStudent.UniversityDataId = ud.Id;
                currentStudent.UniversityData = ud;
                if (!_studentsRepository.AddNewUniversityData(ud))
                    return false;
            }

            var currentDegree = _degreesRepository.GetDegreeById(universityData.DegreeId.ToString());
            if(currentDegree is null)
                return false;
            currentStudent.UniversityData.DegreeId = currentDegree.DegreeId;
            currentStudent.UniversityData.Degree = currentDegree;

            _mapper.Map(universityData, currentStudent.UniversityData);
            var result = _userManager.UpdateAsync(currentStudent).Result;
            return result.Succeeded;
        }

        public bool RemoveStudent(Guid studentId)
        {
            var user = _userManagerStudent.FindByIdAsync(studentId.ToString()).Result;
            if (user is null) return false;
            var validation = _validationRepository.GetValidationById(user.ValidationId);
            var res1 = _context.Remove(validation);
            _context.SaveChanges();
            var res = _userManagerStudent.DeleteAsync(user).Result;
            return res.Succeeded;

        }


        // extras
        private string? GetCurrentUserId()
        {
            var currentUserId= _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            return currentUserId;
        }

        public bool AnyRequiredFieldIsNull(StudentPersonalDataDTO studentPersonalData)
        {
            return studentPersonalData is null ||
                studentPersonalData.UserName == "" ||
                studentPersonalData.FirstName == "" ||
                studentPersonalData.LastName == "" ||
                studentPersonalData.FileNumber == "" ||
                studentPersonalData.Email == "" ||
                studentPersonalData.DocumentTypeId is null ||
                studentPersonalData.DocumentNumber == "" ||
                studentPersonalData.GenderTypeId is null ||
                studentPersonalData.CivilStatusTypeId is null;
        }

    }
}
