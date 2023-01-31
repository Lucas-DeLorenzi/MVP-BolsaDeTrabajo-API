using AutoMapper;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class KnowledgeRepository : IKnowledgeRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BolsaDeTrabajoContext _context;
        private readonly IMapper _mapper;

        public KnowledgeRepository(IHttpContextAccessor httpContextAccessor, BolsaDeTrabajoContext context, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Knowledge>? GetKnowledge()
        {
            var reqId = GetRequesterId();
            return _context.Students
                    .Include(p => p.Knowledgements)
                    .ThenInclude(s => s.KnowledgeType)
                    .Include(p => p.Knowledgements)
                    .ThenInclude(s => s.KnowledgeValue)
                    .FirstOrDefault(p => p.Id == reqId)?.Knowledgements;
                
        }
        public bool AddKnowledge(KnowledgeToInsertDTO knowledgeToInsert)
        {
            var reqID = GetRequesterId();
            if (reqID is null) return false;
            var student = _context.Students.FirstOrDefault(p => p.Id == reqID);
            if (student is null) return false;
            var k = GetKnowledge();

            if(k.Any(p => p.KnowledgeTypeId == knowledgeToInsert.KnowledgeTypeId )) return false;

            var knowledge = new Knowledge();
            knowledge.KnowledgeValueId = knowledgeToInsert.KnowledgeValueId;
            knowledge.KnowledgeTypeId = knowledgeToInsert.KnowledgeTypeId;
            _context.Knowledgments.Add(knowledge);


            if (student.Knowledgements is null)
            {
                student.Knowledgements = new List<Knowledge>();
            }

            student.Knowledgements.Add(knowledge);
            _context.Entry(student).State = EntityState.Modified;
            return SaveChange();
        }

        public bool RemoveKnowledge(Guid knowledgeId)
        {
            var reqID = GetRequesterId();
            var student = _context.Students
                    .Include(p => p.Knowledgements)
                    .ThenInclude(s => s.KnowledgeType)
                    .Include(p => p.Knowledgements)
                    .ThenInclude(s => s.KnowledgeValue)
                    .FirstOrDefault(p => p.Id == reqID);

            var knowledge = student.Knowledgements.FirstOrDefault(p => p.Id == knowledgeId);
            if (knowledge is null) return false;
            student.Knowledgements?.Remove(knowledge);
            _context.Knowledgments.Remove(knowledge);
            
            return SaveChange();

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
