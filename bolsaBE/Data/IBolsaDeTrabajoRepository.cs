using bolsaBE.Entities;

namespace bolsaBE.Services
{
    public interface IBolsaDeTrabajoRepository
    {
        public bool SaveChange();
        IEnumerable<Degree> GetDegrees();
        Degree? GetDegreeById(string idDegree);
        public void AddDegree(Degree degree);
        public void UpdateDegree(Degree degree);
        public void DeleteDegree(Degree degreeToDelete);
    }
}
