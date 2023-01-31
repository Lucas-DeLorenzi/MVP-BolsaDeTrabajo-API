using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> GetAdmin();
        Admin? GetDegreeById(string idDegree);
        public void AddDegree(Admin degree);
        public void UpdateDegree(Admin degree);
        public void DeleteDegree(Admin degreeToDelete);
        public bool SaveChange();
    }
}
