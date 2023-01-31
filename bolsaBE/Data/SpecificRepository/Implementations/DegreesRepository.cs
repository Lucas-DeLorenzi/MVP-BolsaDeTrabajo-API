using AutoMapper;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using Microsoft.EntityFrameworkCore;

namespace bolsaBE.Services
{
    public class DegreesRepository : IDegreesRepository
    {
        private readonly BolsaDeTrabajoContext _context;
        public DegreesRepository(BolsaDeTrabajoContext context)
        {
            _context = context;
        }
        public void AddDegree(Degree degreeToCreate)
        {
            _context.Degrees.Add(degreeToCreate);
        }

        public void DeleteDegree(Degree degreeToDelete)
        {
            _context.Degrees.Remove(degreeToDelete);
        }

        public Degree? GetDegreeById(string idDegree)
        {
            var degree = _context.Degrees.FirstOrDefault(d => d.DegreeId.ToString() == idDegree.ToUpper());
            return degree;
        }

        public IEnumerable<Degree> GetDegrees()
        {
            var degrees = _context.Degrees;
            return degrees;
        }

        public void UpdateDegree(Degree degree)
        {
            _context.Entry(degree).State = EntityState.Modified;
        }

        public bool SaveChange()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
