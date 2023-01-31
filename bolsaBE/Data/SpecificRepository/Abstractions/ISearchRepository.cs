using bolsaBE.Entities;
using bolsaBE.Enums;
using bolsaBE.Models;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface ISearchRepository
    {

        // Create
        bool AddSearchJob(Job jobToCreate);
        bool AddSearchInternship(Internship internshipToCreate);

        // update
        bool ValidateInternship(Guid internshipId);
        bool UpdateJob(JobToUpdateDTO JobToUpdate);
        bool UpdateInternship(InternshipToUpdateDTO internshipToUpdate);

        // Delete
        bool DeleteSearch(Guid searchToDeleteId);
        

        // Get
        IEnumerable<Search> GetSearches(ValidationStatus status);
        IEnumerable<Job> GetSearchJobs();
        IEnumerable<Internship> GetSearchInternships(ValidationStatus status);

        Search? GetSearchById(Guid searchId);
        Internship? GetInternshipById(Guid searchId);
        Job? GetJobById(Guid searchId);

        //Extra
        bool SaveChange();
    }
}
