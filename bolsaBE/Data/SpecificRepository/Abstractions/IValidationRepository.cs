using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IValidationRepository
    {
        public Validation GetValidationById(Guid id);
    }
}
