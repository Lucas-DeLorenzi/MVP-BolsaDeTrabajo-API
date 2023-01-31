using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.Services;

namespace bolsaBE.Data
{
    public interface IUnitOfWork //: IDisposable
    {
        ICivilStatusTypeRepository CivilStatusTypes { get; }
        IDocumentTypeRepository DocumentTypes { get; }
        IGenderTypeRepository GenderTypes { get; }
        IKnowledgeTypeRepository KnowledgeTypes { get; }
        IKnowledgeValueRepository KnowledgeValues { get; }
        IKnowledgeRepository Knowledge { get; }
        IWorkdayTypeRepository WorkdayTypes { get; }
        ISearchRepository Searches { get; }
        IPostulationRepository Postulations { get; }
        IRelationTypesRepository RelationTypes { get; }
        IDegreesRepository Degrees { get; }

        //bool Complete();
    }
}
