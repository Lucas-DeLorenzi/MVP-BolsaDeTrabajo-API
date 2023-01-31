using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.Services;

namespace bolsaBE.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICivilStatusTypeRepository CivilStatusTypes { get; }
        public IDocumentTypeRepository DocumentTypes { get; }
        public IGenderTypeRepository GenderTypes { get; }
        public IKnowledgeTypeRepository KnowledgeTypes { get; }
        public IKnowledgeRepository Knowledge { get; }
        public IWorkdayTypeRepository WorkdayTypes { get; }
        public IKnowledgeValueRepository KnowledgeValues { get; }
        public ISearchRepository Searches { get; }
        public IPostulationRepository Postulations { get; }
        public IRelationTypesRepository RelationTypes { get; }
        public IDegreesRepository Degrees { get; }

        public UnitOfWork(
            ICivilStatusTypeRepository civilStatusTypeRepository,
            IDocumentTypeRepository documentTypeRepository,
            IGenderTypeRepository genderTypeRepository,
            IKnowledgeValueRepository knowledgeValuesRespository,
            IKnowledgeTypeRepository knowledgeTypeRespository,
            IKnowledgeRepository knowledgeRespository,
            IWorkdayTypeRepository workdayTypesRespository,
            ISearchRepository searchRepository,
            IPostulationRepository postulationRepository,
            IRelationTypesRepository relationTypesRepository,
            IDegreesRepository degreesRepository
            )
        {
            this.CivilStatusTypes = civilStatusTypeRepository;
            this.DocumentTypes = documentTypeRepository;
            this.GenderTypes = genderTypeRepository;
            this.KnowledgeValues = knowledgeValuesRespository;
            this.Knowledge = knowledgeRespository;
            this.WorkdayTypes = workdayTypesRespository;
            this.KnowledgeTypes = knowledgeTypeRespository;
            this.Searches = searchRepository;
            this.Postulations = postulationRepository;
            this.RelationTypes = relationTypesRepository;
            this.Degrees = degreesRepository;

        }
        //public bool Complete()
        //{
        //    return _context.SaveChange();
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _context.Dispose();
        //    }
        //}

    }
}
