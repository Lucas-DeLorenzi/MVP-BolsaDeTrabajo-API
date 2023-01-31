using AutoMapper;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Models.Postulations;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class PostulationRepository : IPostulationRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BolsaDeTrabajoContext _context;
        private readonly IMapper _mapper;

        public PostulationRepository(IHttpContextAccessor httpContextAccessor, BolsaDeTrabajoContext context, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<PostulationDTO>? GetPostulations()
        {
            var roleName = GetRequesterRole();
            var reqId = GetRequesterId();
            var postulations = new List<Postulation>();
            switch (roleName)
            {
                case "Administrador":
                    postulations = _context.Postulations
                        .Include(p => p.Student)
                        .Include(p => p.Search)
                        .ThenInclude(s => s.Company)
                        .ToList();
                    break;
                case "Empresa":
                    postulations.AddRange(_context.Postulations
                        .Include(p => p.Search)
                        .Include(p => p.Student)
                        .Where(p => p.Search.CompanyId == reqId)
                        .ToList());
                    break;
                case "Alumno":
                    postulations.AddRange(_context.Postulations
                        .Where(p => p.StudentId == reqId)
                        .Include(p => p.Search)
                        .ThenInclude(s => s.Company)
                        .ToList());
                    break;
                default: return null;
            }

            return _mapper.Map<IEnumerable<PostulationDTO>>(postulations);
        }

        public bool Postulate(Guid searchId)
        {
            if (StudentIsPostulated(searchId)) return false; 
            var postulation = new Postulation();
            postulation.SearchId = searchId;
            var reqID = GetRequesterId();
            if (reqID is null) return false;
            postulation.StudentId = reqID;
            _context.Postulations.Add(postulation);
            return SaveChange();

        }

        public bool UnPostulate(Guid postulationId)
        {
            var postulation = _context.Postulations.FirstOrDefault(p => p.Id == postulationId);
            if (postulation == null) return false;
            _context.Postulations.Remove(postulation);
            return SaveChange();
        }

        public bool StudentIsPostulated(Guid searchId)
        {
            var reqID = GetRequesterId();
            return _context.Postulations.Any(p => p.SearchId == searchId && p.StudentId == reqID);
        }

        private string? GetRequesterRole()
        {
            var roleName = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value;
            return roleName;
        }
        private Guid? GetRequesterId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            if (userId is null) return null;
            return Guid.Parse(userId);
        }
        public bool SaveChange()
        {
            return _context.SaveChanges() >= 0;
        }

    }
}
