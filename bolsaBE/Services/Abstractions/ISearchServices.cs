using bolsaBE.Entities;
using bolsaBE.Enums;
using bolsaBE.Models;
using bolsaBE.Models.Search;

namespace bolsaBE.Services.Abstractions
{
    public interface ISearchServices
    {
        // create
        bool CreateJobSearch(JobToCreateDTO jobToCreateDTO);
        bool CreateInternShipSearch(InternshipToCreateDTO internshipToCreateDTO);

        //update
        bool ValidateInternship(Guid internshipId);
        bool UpdateJob(JobToUpdateDTO JobToUpdate);
        bool UpdateInternship(InternshipToUpdateDTO internshipToUpdate);

        // delete
        bool DeleteSearch(Guid searchToDeleteId);
        

        // GET
        IEnumerable<SearchDTO> GetSearches(ValidationStatus status);
        IEnumerable<JobDTO> GetJobSearches();
        IEnumerable<InternshipDTO> GetInternshipSearches(ValidationStatus status);

        SearchDTO? GetSearchById(Guid searchId);
        InternshipDTO? GetInternshipById(Guid searchId);
        JobDTO? GetJobById(Guid searchId);
    }
}
