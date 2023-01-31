using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface ICompanyRepository
    {
        public IEnumerable<Company> GetCompanies();
    }
}
