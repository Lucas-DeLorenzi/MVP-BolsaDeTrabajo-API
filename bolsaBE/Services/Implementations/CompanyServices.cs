using AutoMapper;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Models.Users.Company;
using bolsaBE.Services.Abstractions;
using bolsaBE.Services.MailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bolsaBE.Services.Implementations
{
    public class CompanyServices : ICompanyServices
    {
        private readonly UserManager<User> _userManager;
        private readonly UserManager<Company> _userManagerCompany;
        private readonly UserManager<Admin> _userManagerAdmin;
        private readonly BolsaDeTrabajoContext _context;
        private readonly ICompanyRepository _companyRepository;
        private readonly IValidationRepository _validationRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ISystemSupportMail _systemSupportMail;
        public CompanyServices(
            UserManager<User> userManager,
            UserManager<Company> userManagerCompany,
            UserManager<Admin> userManagerAdmin,
            BolsaDeTrabajoContext context,
            ICompanyRepository companyRepository,
            IValidationRepository validationRepository,
            IAddressRepository addressRepository,
            IContactRepository contactRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            ISystemSupportMail systemSupportMail
            )
        {
            _userManager = userManager;
            _context = context;
            _userManagerCompany = userManagerCompany;
            _userManagerAdmin = userManagerAdmin;    
            _validationRepository = validationRepository;
            _addressRepository = addressRepository;
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _systemSupportMail = systemSupportMail;
        }
        public async Task<bool> CreateCompany(CompanyToCreateDTO company)
        {
            Company c = new Company(company.BusinessName)
            {
                UserName = company.Email,
                Email = company.Email
            };
            var user = await _userManager.CreateAsync(c, company.Password);
            var role = await _userManager.AddToRoleAsync(c, "Empresa");

            if(!user.Succeeded || !role.Succeeded)
                return false;
            string sbj = "Bienvenido a la Bolsa de Trabajo de la UTN";
            string body = MailBodyHelper.MailGenericWelcomeBody(company.BusinessName);

            _systemSupportMail.SendEmail(sbj,body,company.Email);

            return true;

        }


        public IEnumerable<CompanyDTO> GetCompanies()
        {
            return _mapper.Map<IEnumerable<CompanyDTO>>(_companyRepository.GetCompanies());
        }

        public CompanyDataDTO? GetCurrentCompanyData()
        {
            var company = _userManagerCompany.FindByIdAsync(GetCurrentUserId()).Result;
            company.Address = _addressRepository.GetAddressById(company.AddressId);
            company.Contact = _contactRepository.GetContactById(company.ContactId);
            var currentCompany = _mapper.Map<CompanyDataDTO>(company);
            return currentCompany;
        }
        public IEnumerable<CompanyToValidateDTO> GetCompaniesToValidate()
        {
            return _companyRepository.GetCompanies().Where(s =>
                    s.BusinessName != null &&
                    s.CuilCuit != null &&
                    s.Sector != null &&
                    s.PhoneNumber != null &&
                    s.Address != null &&
                    s.Address.Street != null &&
                    s.Address.StreetNumber != null &&
                    s.Address.City != null &&
                    s.Address.PostalCode != null &&
                    s.Contact != null &&
                    s.Contact.Name != null &&
                    s.Contact.RelationType != null &&
                    s.Contact.Email != null &&
                    s.Contact.Phone != null &&
                    s.Contact.Position != null &&
                    s.Validation.UpdatedAt is null
                    )
                    .Select(s => new CompanyToValidateDTO { Name = s.BusinessName, Id = s.Id, Role = "EMPRESA", Path = "company" }).ToList();
        }

        public IEnumerable<CompanyUniqueFieldsDTO> GetCompaniesUniqueFields()
        {
            var companies = _userManager.GetUsersInRoleAsync("Empresa").Result;
            return _mapper.Map<IEnumerable<CompanyUniqueFieldsDTO>>(companies);
        }
                
        public string PutUpdatedAtForCompany(string id)
        {
            Company? company = null;
            try
            {
                company = _userManagerCompany.FindByIdAsync(id).Result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null)
                return "Not Found";
            company.Validation = _validationRepository.GetValidationById(company.ValidationId);
            if (company.Validation.UpdatedAt != null)
                return "La empresa ya ha sido validada";
            var admin = _userManagerAdmin.FindByIdAsync(GetCurrentUserId()).Result;
            company.Validation.UpdatedAt = DateTime.Now;
            company.Validation.Responsible = admin;
            var validatedCompany = _userManager.UpdateAsync(company).Result;

            if (validatedCompany.Succeeded)
            {
                string sbj = "Has sido validado en la Bolsa de Trabajo de la UTN";
                string body = MailBodyHelper.MailGenericValidateBody(company.BusinessName);

                _systemSupportMail.SendEmail(sbj, body, company.Email);
                return "Ok";
            }
            return "Error";

        }

        public bool RemoveCompany(Guid companyId)
        {
            var user = _userManagerCompany.FindByIdAsync(companyId.ToString()).Result;
            if (user is null) return false;
            var validation = _context.Validations.FirstOrDefault(v => v.Id == user.ValidationId);
            var res = _context.Remove(user);
            var result = _context.SaveChanges();
            var res1 = _context.Remove(validation);
            var result2 = _context.SaveChanges();
            return result > 0;
        }

        public bool UpdateCompanyData(CompanyDataDTO companyData)
        {
            var currentCompany = _userManagerCompany.FindByIdAsync(GetCurrentUserId()).Result;
            if (currentCompany.AddressId != null)
            {
                currentCompany.Address = _addressRepository.GetAddressById(currentCompany.AddressId);
            }
            else
            {
                var address = new Address();
                currentCompany.Address = address;
                currentCompany.AddressId = address.Id;
                if (!_addressRepository.AddAddress(address))
                    return false;
            }
            if (currentCompany.ContactId != null)
            {
                currentCompany.Contact = _contactRepository.GetContactById(currentCompany.ContactId);
            }
            else
            {
                var contact = new Contact();
                currentCompany.Contact = contact;
                currentCompany.ContactId = contact.Id;
                if (!_contactRepository.AddContact(contact))
                    return false;
            }

            _mapper.Map(companyData.Address, currentCompany.Address);
            _mapper.Map(companyData.Contact, currentCompany.Contact);

            var company = _mapper.Map(companyData, currentCompany);
            var result = _userManager.UpdateAsync(company).Result;
            return result.Succeeded;
        }

        public bool AnyRequiredFieldIsNull(CompanyDataDTO companyData)
        {
            return companyData is null ||
                companyData.BusinessName == "" ||
                companyData.CuilCuit == "" ||
                companyData.Sector == "" ||
                companyData.PhoneNumber == "" ||
                companyData.Address is null ||
                companyData.Address.Street == "" ||
                companyData.Address.StreetNumber == "" ||
                companyData.Address.City == "" ||
                companyData.Address.PostalCode == "" ||
                companyData.Contact is null ||
                companyData.Contact.FirstName == "" ||
                companyData.Contact.LastName == "" ||
                companyData.Contact.RelationTypeId is null ||
                companyData.Contact.Email == "" ||
                companyData.Contact.Phone == "" ||
                companyData.Contact.Position == "";
        }
        private string? GetCurrentUserId()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            return currentUserId;
        }
    }
}
