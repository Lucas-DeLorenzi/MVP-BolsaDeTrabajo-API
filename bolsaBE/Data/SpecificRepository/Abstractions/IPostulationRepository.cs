using bolsaBE.Models.Postulations;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IPostulationRepository
    {
        IEnumerable<PostulationDTO>? GetPostulations();
        bool Postulate(Guid searchId);
        bool UnPostulate(Guid postulationId);
    }
}
