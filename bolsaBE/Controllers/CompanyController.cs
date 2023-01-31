using bolsaBE.Models.Users.Company;
using bolsaBE.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bolsaBE.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase 
    {
        private readonly ICompanyServices _companyServices;
        public CompanyController (ICompanyServices companyServices)
        {
            _companyServices = companyServices;
        }


        [HttpGet]
        public ActionResult GetCompanies()
        {
            return Ok(_companyServices.GetCompanies());

        }

        [HttpGet("GetCurrentCompanyData")]
        [Authorize(Roles = "Empresa")]
        public ActionResult<CompanyDTO> GetCurrentCompanyData()
        {

            var company = _companyServices.GetCurrentCompanyData();
            if (company is null)
                return NotFound();
            return Ok(company);
        }

        [HttpGet("toValidate")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<IEnumerable<CompanyToValidateDTO>> GetCompaniesToValidate()
        {
            var companies = _companyServices.GetCompaniesToValidate();
            return Ok(companies);
        }


        [HttpPut("toValidate/{id}", Name = "PutUpdatedAtForCompany")]
        [Authorize(Roles = "Administrador")]
        public ActionResult PutUpdatedAtForCompany(string id)
        {
            var result = _companyServices.PutUpdatedAtForCompany(id);
            switch (result)
            {
                case "Ok":
                    return Ok("La empresa ha sido validada");
                case "Not Found":
                    return NotFound();
                default:
                    return BadRequest(result);
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddCompany(CompanyToCreateDTO company)
        {
            try
            {
                if (company == null)
                {
                    return BadRequest("Ingrese una empresa");
                }
                var task = _companyServices.CreateCompany(company).Result;
                if (task)
                {
                    return Ok("Success");
                }
                return BadRequest("Algo salio mal");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("updateCompanyData")]
        [Authorize(Roles = "Empresa")]
        public ActionResult UpdateCompanyData(CompanyDataDTO companyData)
        {
            try
            {
                if (_companyServices.AnyRequiredFieldIsNull(companyData))
                    return BadRequest("Los campos obligatorios no pueden ser nulos");
                if (_companyServices.UpdateCompanyData(companyData))
                    return Ok("Usuario actualizado");
                return BadRequest("Algo salio mal");
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpDelete("{companyId}")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<bool> RemoveCompany(Guid companyId)
        {
            try
            {
                return Ok(_companyServices.RemoveCompany(companyId));

            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetCompaniesUniqueFields")]
        [Authorize(Roles = "Empresa")]
        public ActionResult <IEnumerable<CompanyUniqueFieldsDTO>> GetCompaniesUniqueFields()
        {
            return Ok(_companyServices.GetCompaniesUniqueFields());
        }
    }
}
