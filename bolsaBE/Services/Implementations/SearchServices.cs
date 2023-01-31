using AutoMapper;
using bolsaBE.Data;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Entities.Auxiliary.Types.Implementation;
using bolsaBE.Enums;
using bolsaBE.Models;
using bolsaBE.Models.Search;
using bolsaBE.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace bolsaBE.Services.Implementations
{
    public class SearchServices : ISearchServices
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly UserManager<Company> _userManagerCompany;
        private IHttpContextAccessor _httpContextAccessor;
        private BolsaDeTrabajoContext _context;


        public SearchServices(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            UserManager<Company> userManagerCompany,
            IHttpContextAccessor httpContextAccessor,
            BolsaDeTrabajoContext context
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManagerCompany = userManagerCompany;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        // create
        public bool CreateJobSearch(JobToCreateDTO jobToCreateDTO)
        {
            var job = _mapper.Map<Job>(jobToCreateDTO);
            if(jobToCreateDTO.DegreesId is not null && jobToCreateDTO.DegreesId.Count > 0)
            {
                var degrees = jobToCreateDTO.DegreesId.Select(d => _unitOfWork.Degrees.GetDegreeById(d)).ToList();
                job.Degrees = degrees;
            }

            if (jobToCreateDTO.KnowledgementTypeId is not null && jobToCreateDTO.KnowledgementTypeId.Count > 0)
            {
                var knowledgments = jobToCreateDTO.KnowledgementTypeId.Select(d => _unitOfWork.KnowledgeTypes.GetKnowledgeById(d)).ToList();
                job.KnowledgeTypes = knowledgments;
            }
            Guid companyId = GetCurrentCompanyId();
            job.CompanyId = companyId;
            job.EmailReciever = GetCurrentCompanyEmail();
            _unitOfWork.Searches.AddSearchJob(job);
            return _unitOfWork.Searches.SaveChange();
        }
        public bool CreateInternShipSearch(InternshipToCreateDTO internshipToCreateDTO)
        {
            var internship = _mapper.Map<Internship>(internshipToCreateDTO);
            if (internshipToCreateDTO.KnowledgementTypeId is not null && internshipToCreateDTO.KnowledgementTypeId.Count > 0)
            {
                var knowledgments = internshipToCreateDTO.KnowledgementTypeId.Select(d => _unitOfWork.KnowledgeTypes.GetKnowledgeById(d)).ToList();
                internship.KnowledgeTypes = knowledgments;
            }

            if (internshipToCreateDTO.DegreesId is not null && internshipToCreateDTO.DegreesId.Count > 0)
            {
                var degrees = internshipToCreateDTO.DegreesId.Select(d => _unitOfWork.Degrees.GetDegreeById(d)).ToList();
                internship.Degrees = degrees;
            }
            Guid companyId = GetCurrentCompanyId();
            internship.CompanyId = companyId;
            internship.EmailReciever = GetCurrentCompanyEmail();
            var result = _unitOfWork.Searches.AddSearchInternship(internship);

            return result;
        }

        // update
        public bool ValidateInternship(Guid internshipId)
        {
            return _unitOfWork.Searches.ValidateInternship(internshipId);
        }

        public bool UpdateJob(JobToUpdateDTO JobToUpdate)
        {
            return _unitOfWork.Searches.UpdateJob(JobToUpdate);
        }
        public bool UpdateInternship(InternshipToUpdateDTO internshipToUpdate)
        {
            return _unitOfWork.Searches.UpdateInternship(internshipToUpdate);
        }

        // delete

        public bool DeleteSearch(Guid searchToDeleteId)
        {
            return _unitOfWork.Searches.DeleteSearch(searchToDeleteId);
        }


        // GET
        public IEnumerable<SearchDTO> GetSearches(ValidationStatus status)
        {
            var search = _unitOfWork.Searches.GetSearches(status);
            return _mapper.Map<IEnumerable<SearchDTO>>(search);
        }

        public IEnumerable<JobDTO> GetJobSearches()
        {
            var jobs = _unitOfWork.Searches.GetSearchJobs();
            return _mapper.Map<IEnumerable<JobDTO>>(jobs);
        }

        public IEnumerable<InternshipDTO> GetInternshipSearches(ValidationStatus status)
        {
            var internships = _unitOfWork.Searches.GetSearchInternships(status);
            return _mapper.Map<IEnumerable<InternshipDTO>>(internships);
        }

        public SearchDTO? GetSearchById(Guid searchId)
        {
            return _mapper.Map<SearchDTO>(_unitOfWork.Searches.GetSearchById(searchId));
        }
        public InternshipDTO? GetInternshipById(Guid searchId)
        {
            return _mapper.Map<InternshipDTO>(_unitOfWork.Searches.GetInternshipById(searchId));
        }

        public JobDTO? GetJobById(Guid searchId)
        {
            return _mapper.Map<JobDTO>(_unitOfWork.Searches.GetJobById(searchId));
        }
        
        //extras

        private Guid GetCurrentCompanyId()
        {
            var companyId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            return Guid.Parse(companyId);
        }

        private string GetCurrentCompanyEmail()
        {
            var companyEmail = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email))?.Value;
            return companyEmail;
        }

    }
}
