using bolsaBE.Entities;

namespace bolsaBE.Services
{
    public interface IDegreesRepository
    {
        IEnumerable<Degree> GetDegrees();
        Degree? GetDegreeById(string idDegree);
        public void AddDegree(Degree degree);
        public void UpdateDegree(Degree degree);
        public void DeleteDegree(Degree degreeToDelete);
        public bool SaveChange();
    }
}
