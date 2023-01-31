using AutoMapper;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Enums;
using bolsaBE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Claims;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class SearchRepository : ISearchRepository
    {
        private readonly UserManager<Admin> _userManagerAdmin;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BolsaDeTrabajoContext _context;
        private readonly IMapper _mapper;

        public SearchRepository(
            UserManager<Admin> userManagerAdmin,
            BolsaDeTrabajoContext context,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
            )
        {
            _userManagerAdmin = userManagerAdmin;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        // Create

        public bool AddSearchJob(Job jobToCreate)
        {
            _context.Jobs.Add(jobToCreate);
            return SaveChange();
        }

        public bool AddSearchInternship(Internship internshipToCreate)
        {
            _context.Internships.Add(internshipToCreate);
            return SaveChange();
        }

        // update

        public bool ValidateInternship(Guid internshipId)
        {
            var userName = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type.ToString().Equals("username"))?.Value;
            var admin = _userManagerAdmin.FindByNameAsync(userName).Result;
            if (admin == null) return false;

            var internship = _context.Internships.FirstOrDefault(i => i.Id == internshipId);
            if (internship == null) return false;

            var validation = _context.Validations.FirstOrDefault(v => v.Id == internship.ValidationId);

            validation.Responsible = admin;
            validation.UpdatedAt = DateTime.Now;
            internship.DiscriminatorValue = "Pasantía";
            _context.Entry(internship).State = EntityState.Modified;
            _context.Entry(validation).State = EntityState.Modified;
            return SaveChange();
        }

        public bool UpdateInternship(InternshipToUpdateDTO internshipToUpdate)
        {
            var internship = _mapper.Map<Internship>(internshipToUpdate);
            var validation = _context.Validations.FirstOrDefault(v => v.Id == internship.ValidationId);
            if (validation == null) return false;

            validation.AdminId = null;
            validation.UpdatedAt = null;
            internship.DiscriminatorValue = "Pasantía a Verificar";

            _context.Entry(internship).State = EntityState.Modified;
            _context.Entry(validation).State = EntityState.Modified;
            return SaveChange();
        }

        public bool UpdateJob(JobToUpdateDTO jobToUpdate)
        {
            var job = _mapper.Map<Job>(jobToUpdate);
            _context.Entry(job).State = EntityState.Modified;
            return SaveChange();
        }

        // Delete

        public bool DeleteSearch(Guid searchToDeleteId)
        {
            var search = GetSearchById(searchToDeleteId);
            if (search is null) return false;

            if(search.DiscriminatorValue == "Empleo")
            {
                if (!_context.Jobs.Remove((Job)search).IsKeySet) return false;
            }
            if (search.DiscriminatorValue == "Pasantía" || search.DiscriminatorValue == "Pasantía a Verificar")
            {
                var internship = _context.Internships.FirstOrDefault(i => i.Id == search.Id);
                if (internship is null) return false;
                var validation = _context.Validations.FirstOrDefault(v => v.Id == internship.ValidationId);
                if (validation is null) return false;
                _context.Validations.Remove(validation);
                if (!_context.Internships.Remove(internship).IsKeySet) return false;

            }

            if (SaveChange()) return true;

            return false;

        }

        // GET

        public IEnumerable<Job> GetSearchJobs()
        {
            var jobs = _context.Jobs
                .Where(s => s.DateUntil > DateTime.Now)
                .Include(company => company.Company)
                .Include(p => p.Postulations)
                .Include(k => k.KnowledgeTypes)
                .Include(d => d.Degrees)
                .ToList();

            // Alternativa sin Include
            foreach (var job in jobs)
            {
                var wdt = _context.WorkdayTypes.FirstOrDefault(x => x.Id == job.WorkdayTypeId);
                job.WorkdayType = wdt;
            }

            return jobs;
        }

        public IEnumerable<Internship> GetSearchInternships(ValidationStatus status)
        {
            switch (status)
            {
                case ValidationStatus.All:
                    return _context.Internships
                        .Where(s => s.DateUntil > DateTime.Now)
                        .Include(company => company.Company)
                        .Include(validation => validation.Validation)
                        .Include(p => p.Postulations)
                        .Include(k => k.KnowledgeTypes)
                        .ToList();
                case ValidationStatus.Validated:
                    return _context.Internships
                       .Include(company => company.Company)
                       .Include(validation => validation.Validation)
                       .Include(p => p.Postulations)
                       .Include(k => k.KnowledgeTypes)
                       .Where(i => i.Validation.UpdatedAt != null && i.DateUntil > DateTime.Now)
                       .ToList();
                case ValidationStatus.ToValidate:
                    return _context.Internships
                       .Include(company => company.Company)
                       .Include(p => p.Postulations)
                       .Include(validation => validation.Validation)
                       .Include(k => k.KnowledgeTypes)
                       .Where(i => i.Validation.UpdatedAt == null && i.DateUntil > DateTime.Now)
                       .ToList();
                default:
                    return new List<Internship>();
            }
           
        }

        public IEnumerable<Internship> GetValidatedInternships()
        {
            return _context.Internships
                .Include(company => company.Company)
                .Include(validation => validation.Validation)
                .Include(k => k.KnowledgeTypes)
                .Where(i => i.Validation.UpdatedAt != null && i.DateUntil > DateTime.Now)
                .ToList();
        }

        public IEnumerable<Search> GetSearches(ValidationStatus status)
        {
            var searches = new List<Search>();
            switch (status)
            {
                case ValidationStatus.All:
                    searches = _context.Searches
                        .Include(k => k.KnowledgeTypes)
                        .Include(company => company.Company)
                            .ThenInclude(v => v.Validation)
                        .Include(s => s.Postulations)
                        .Include(d => d.Degrees)
                        .Where(s => s.DateUntil > DateTime.Now)
                        .ToList();
                    break;
                case ValidationStatus.Validated:
                    searches = _context.Searches
                        .Where(i => i.DiscriminatorValue != "Pasantía a Verificar" && i.DateUntil > DateTime.Now)
                        .Include(company => company.Company)
                            .ThenInclude(v => v.Validation)
                        .Include(s => s.Postulations)
                        .Include(d => d.Degrees)
                        .Include(k => k.KnowledgeTypes)
                        .ToList();
                    break;
                case ValidationStatus.ToValidate:
                    searches = _context.Searches
                        .Where(i => i.DiscriminatorValue != "Pasantía" && i.DateUntil > DateTime.Now)
                        .Include(company => company.Company)
                            .ThenInclude(v => v.Validation)
                        .Include(s => s.Postulations)
                        .Include(d => d.Degrees)
                        .Include(k => k.KnowledgeTypes)
                        .ToList();
                    break;
                default:
                    return Enumerable.Empty<Search>();
            }
            var currentUserRole = GetRequesterRole();
            if (currentUserRole == "Empresa")
            {
                var userId = GetRequesterId();
                searches = searches.Where(s => s.Company.Id == userId).ToList();
            }

            return searches;
        }

        public Search? GetSearchById(Guid searchId)
        {
            return _context.Searches
                .Include(company => company.Company)
                .Include(k => k.KnowledgeTypes)
                .FirstOrDefault(search=> search.Id == searchId);
        }
        
        public Internship? GetInternshipById(Guid searchId)
        {
            return _context.Internships
                .Include(company => company.Company)
                .Include(k => k.KnowledgeTypes)
                .Include(p => p.Postulations)
                    .ThenInclude(s => s.Student)
                        .ThenInclude(s => s.Validation)
                .Include(p => p.Postulations)
                    .ThenInclude(s => s.Student)
                        .ThenInclude(od => od.OtherData)
                .Include(p => p.Postulations)
                    .ThenInclude(s => s.Student)
                        .ThenInclude(od => od.DocumentType)
                .Include(p => p.Postulations)
                    .ThenInclude(s => s.Student)
                        .ThenInclude(od => od.GenderType)
                .FirstOrDefault(search => search.Id == searchId);
        }

        public Job? GetJobById(Guid searchId)
        {
            return _context.Jobs
                .Include(company => company.Company)
                .Include(k => k.KnowledgeTypes)
                .Include(wdt => wdt.WorkdayType)
                .Include(p => p.Postulations)
                    .ThenInclude(s => s.Student)
                        .ThenInclude(s => s.Validation)
                .Include(p => p.Postulations)
                    .ThenInclude(s => s.Student)
                        .ThenInclude(od => od.OtherData)
                .Include(p => p.Postulations)
                    .ThenInclude(s => s.Student)
                        .ThenInclude(od => od.DocumentType)
                .Include(p => p.Postulations)
                    .ThenInclude(s => s.Student)
                        .ThenInclude(od => od.GenderType)
                .FirstOrDefault(search => search.Id == searchId);
        }

        // EXTRAS
        public bool SaveChange()
        {
            return _context.SaveChanges() >= 0;
        }
        private string? GetRequesterRole()
        {
            var roleName = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role))?.Value;
            return roleName;
        }
        private Guid? GetRequesterId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            if (userId is null) return null;
            return Guid.Parse(userId);
        }
    }
}
