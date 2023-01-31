using bolsaBE.Data.GenericRepository;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using Microsoft.EntityFrameworkCore;

namespace bolsaBE.Services
{
    public class BolsaDeTrabajoRepository : IBolsaDeTrabajoRepository
    {
        private readonly BolsaDeTrabajoContext _context;
        private readonly IDegreesRepository _degreesRepository;
        public BolsaDeTrabajoRepository(
            BolsaDeTrabajoContext context, 
            IDegreesRepository degreesRepository)
        {
            _context = context;
            _degreesRepository = degreesRepository;
        }

        // General
        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }

        // Degrees
        public IEnumerable<Degree> GetDegrees()
        {
            return _degreesRepository.GetDegrees();
        }
        public void AddDegree(Degree degree)
        {
            throw new NotImplementedException();
        }

        public void DeleteDegree(Degree degreeToDelete)
        {
            throw new NotImplementedException();
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }

        public Degree? GetDegreeById(string idDegree)
        {
            throw new NotImplementedException();
        }



        public void UpdateDegree(Degree degree)
        {
            throw new NotImplementedException();
        }
    }
}
