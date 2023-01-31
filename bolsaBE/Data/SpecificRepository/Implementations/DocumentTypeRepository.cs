using bolsaBE.Data.GenericRepository;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Entities.Generics;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class DocumentTypeRepository : GenericRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(BolsaDeTrabajoContext dbContext) : base(dbContext)
        {

        }
    }
}
