using bolsaBE.Models.Postulations;

namespace bolsaBE.Services.Abstractions
{
    public interface IPostulationServices
    {
        IEnumerable<PostulationDTO>? GetPostulations();
        bool Postulate(Guid searchId);
        bool UnPostulate(Guid postulationId);
    }
}
