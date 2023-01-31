using bolsaBE.Models.Users.Company;

namespace bolsaBE.Services.Abstractions
{
    public interface ICompanyServices
    {
        Task<bool> CreateCompany(CompanyToCreateDTO company);
        IEnumerable<CompanyToValidateDTO> GetCompaniesToValidate();
        public CompanyDataDTO? GetCurrentCompanyData();
        IEnumerable<CompanyDTO> GetCompanies();
        public IEnumerable<CompanyUniqueFieldsDTO> GetCompaniesUniqueFields();
        string PutUpdatedAtForCompany(string id);
        bool RemoveCompany(Guid companyId);
        public bool UpdateCompanyData(CompanyDataDTO companyData);
        bool AnyRequiredFieldIsNull(CompanyDataDTO companyData);
    }
}
