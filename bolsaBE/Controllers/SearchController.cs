using AutoMapper;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.Entities;
using bolsaBE.Models;
using bolsaBE.Models.Search;
using bolsaBE.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bolsaBE.Controllers
{
    [Route("api/searches")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchServices _searchServices;

        public SearchController(
            ISearchServices searchServices
            )
        {
            _searchServices = searchServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SearchDTO>> GetSearches(Enums.ValidationStatus validationStatus = Enums.ValidationStatus.Validated)
        {
            var searches = _searchServices.GetSearches(validationStatus);
            return Ok(searches);
        }

        [HttpGet("jobs")]
        public ActionResult<IEnumerable<JobDTO>> GetJobs()
        {
            var jobs = _searchServices.GetJobSearches();
            return Ok(jobs);
        }

        [HttpGet("internships")]
        public ActionResult<IEnumerable<InternshipDTO>> GetInternships(Enums.ValidationStatus validationStatus = Enums.ValidationStatus.All)
        {
            var internships = _searchServices.GetInternshipSearches(validationStatus);
            return Ok(internships);
        }

        [HttpGet("internships/{searchId}")]
        public ActionResult<InternshipDTO> GetInternshipById(Guid searchId)
        {
            var internship = _searchServices.GetInternshipById(searchId);
            return Ok(internship);
        }

        [HttpGet("jobs/{searchId}")]
        public ActionResult<InternshipDTO> GetJobById(Guid searchId)
        {
            var job = _searchServices.GetJobById(searchId);
            if (job == null) return NotFound();
            return Ok(job);
        }

        // Eliminar luego de verificar la implementación en frontend

        //[HttpGet("validatedInternships")]
        //public ActionResult<IEnumerable<InternshipDTO>> GetValidatedInternships()
        //{
        //    var internships = _searchServices.GetInternshipSearches(Enums.ValidationStatus.Validated);
        //    return Ok(internships);
        //}

        //[HttpGet("internshipsToValidate")]
        //public ActionResult<IEnumerable<InternshipDTO>> GetInternshipsToValidate()
        //{
        //    return Ok(_searchServices.GetInternshipSearches(Enums.ValidationStatus.ToValidate));
        //}

        [HttpPost("job")]
        [Authorize(Roles = "Empresa")]
        public ActionResult<bool> AddJob(JobToCreateDTO jobToCreate)
        {
            bool created = _searchServices.CreateJobSearch(jobToCreate);

            if (!created)
            {
                return BadRequest("No se pudo crear la búsqueda, revise los datos ingresados");
            }
            return Ok();
        }

        [HttpPost("internship")]
        [Authorize(Roles = "Empresa")]
        public ActionResult<bool> AddInternship(InternshipToCreateDTO internshipToCreate)
        {

            bool created = _searchServices.CreateInternShipSearch(internshipToCreate);

            if (!created)
            {
                return BadRequest("No se pudo crear la búsqueda, revise los datos ingresados");
            }
            return Ok();
        }

        [HttpPut("internshipsToValidate/{id}")]
        [Authorize(Roles = "Administrador")]
        public ActionResult ValidateInternships(Guid id)
        {
            return Ok(_searchServices.ValidateInternship(id));
        }

        [HttpPut("jobs")]
        //[Authorize(Roles = "Administrador")]
        public ActionResult UpdateJob(JobToUpdateDTO jobToUpdate)
        {
            return Ok(_searchServices.UpdateJob(jobToUpdate));
        }

        [HttpPut("internships")]
        //[Authorize(Roles = "Administrador")]
        public ActionResult UpdateInternship(InternshipToUpdateDTO internshipToUpdateDTO)
        {
            return Ok(_searchServices.UpdateInternship(internshipToUpdateDTO));
        }

        [HttpDelete("{idSearch}")]
        [Authorize(Roles = "Administrador, Empresa")]
        public ActionResult DeleteSearch(Guid idSearch)
        {
            try
            {
                if (_searchServices.DeleteSearch(idSearch))
                {
                    return NoContent();
                };
                return BadRequest();
            } catch
            {
                return BadRequest();
            }
        }


    }
}
