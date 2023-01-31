using bolsaBE.Entities;
using bolsaBE.Models;

namespace bolsaBE.Services
{
    public interface IDegreesServices
    {
        public IEnumerable<DegreeDTO> GetDegrees();
        public DegreeDTO GetDegreeById(string idDegree);
        public DegreeDTO? AddDegree(DegreeToCreateDTO degree);
        public bool UpdateDegree(DegreeToUpdDTO degree, string idDegree);
        public bool DeleteDegree(string idDegree);
    }
}
