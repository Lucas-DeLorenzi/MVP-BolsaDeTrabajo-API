using AutoMapper;
using bolsaBE.Data;
using bolsaBE.Entities;
using bolsaBE.Models;
using bolsaBE.Services.Abstractions;


namespace bolsaBE.Services.Implementations
{
    public class KnowledgeServices : IKnowledgeServices
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        public KnowledgeServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<KnowledgeDTO>? GetKnowledge()
        {
            return _mapper.Map<IEnumerable<KnowledgeDTO>>(_unitOfWork.Knowledge.GetKnowledge());
        }

        public bool AddKnowledge(KnowledgeToInsertDTO knowledgeToInsert)
        {
            
            return _unitOfWork.Knowledge.AddKnowledge(knowledgeToInsert);

        }


        public bool RemoveKnowledge(Guid knowledgeId)
        {
            return (_unitOfWork.Knowledge.RemoveKnowledge(knowledgeId));
        }
    }
}
