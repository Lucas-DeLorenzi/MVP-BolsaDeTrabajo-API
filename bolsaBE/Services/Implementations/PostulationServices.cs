using bolsaBE.Data;
using bolsaBE.Models.Postulations;
using bolsaBE.Services.Abstractions;
using bolsaBE.Services.MailServices;

namespace bolsaBE.Services.Implementations
{
    public class PostulationServices : IPostulationServices

    {
        private readonly ISystemSupportMail _systemSupportMail;
        private IUnitOfWork _unitOfWork;
        
  
        public PostulationServices(IUnitOfWork unitOfWork, ISystemSupportMail systemSupportMail)
        {
            _unitOfWork = unitOfWork;
            _systemSupportMail = systemSupportMail;
        }

        public IEnumerable<PostulationDTO>? GetPostulations()
        {
            return _unitOfWork.Postulations.GetPostulations();
        }

        public bool Postulate(Guid searchId)
        {
            var search = _unitOfWork.Searches.GetSearchById(searchId);

            if (_unitOfWork.Postulations.Postulate(searchId))
            {
                string sbj = "Aviso de nueva postulación - UTN Bolsa de Trabajo";
                string body = $"Usted ha recibido una nueva postulación para la búsqueda: {search.SearchTitle}";
                _systemSupportMail.SendEmail(sbj, body, search.Company.Email);
                return true;
            }
            return false;

        }

        public bool UnPostulate(Guid postulationId)
        {
            return _unitOfWork.Postulations.UnPostulate(postulationId);
        }
    }
}
