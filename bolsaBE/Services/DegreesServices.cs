using AutoMapper;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Models;

namespace bolsaBE.Services
{
    public class DegreesServices : IDegreesServices
    {
        private readonly IDegreesRepository _degreesRepository;
        private readonly IMapper _mapper;
        public DegreesServices(IDegreesRepository degreesRepository, IMapper mapper)
        {
            _degreesRepository = degreesRepository;
            _mapper = mapper;
        }
        public DegreeDTO? AddDegree(DegreeToCreateDTO degreeToCreate)
        {
            var degree = _mapper.Map<Degree>(degreeToCreate);
            var degreeExists = _degreesRepository.GetDegrees().FirstOrDefault(d => d.DegreeTitle.ToLower() == degreeToCreate.DegreeTitle.ToLower());
            if (degreeToCreate is null || degreeExists is not null)
            {
                return null;
            }
            _degreesRepository.AddDegree(degree);
            if (_degreesRepository.SaveChange())
            {
                return _mapper.Map<DegreeDTO>(degree);
            }
            return null;
        }

        public bool DeleteDegree(string idDegree)
        {
            var degreeToDelete = _degreesRepository.GetDegreeById(idDegree);
            if (degreeToDelete is null)
            {
                return false;
            }
            _degreesRepository.DeleteDegree(degreeToDelete);
            if (_degreesRepository.SaveChange())
            {
                return true;
            }
            return false;
        }

        public DegreeDTO GetDegreeById(string idDegree)
        {
            var degree = _degreesRepository.GetDegreeById(idDegree);
            return _mapper.Map<DegreeDTO>(degree);
        }

        public IEnumerable<DegreeDTO> GetDegrees()
        {
            var degrees = _degreesRepository.GetDegrees();
            return _mapper.Map<IEnumerable<DegreeDTO>>(degrees);
        }

        public bool UpdateDegree(DegreeToUpdDTO degree, string degreeId)
        {
            var degreeToUpdate = _degreesRepository.GetDegreeById(degreeId);
            if (degreeToUpdate is null)
            {
                return false;
            }
            _mapper.Map(degree, degreeToUpdate);
            _degreesRepository.UpdateDegree(degreeToUpdate);
            if (_degreesRepository.SaveChange())
            {
                return true;
            };
            return false;
        }
    }
}
