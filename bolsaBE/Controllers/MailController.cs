using AutoMapper;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Services.MailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bolsaBE.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly UserManager<Student> _userManagerStudent;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BolsaDeTrabajoContext _context;
        private readonly IMapper _mapper;

        public MailController(
            UserManager<Student> userManagerStudent,
            BolsaDeTrabajoContext context,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
            )
        {
            _userManagerStudent = userManagerStudent;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult SendingMail(string email) 
        {

            var students = _context.Students
                                    .Include(s => s.OtherData)
                                    .Where(s => s.OtherData != null && s.OtherData.Curriculum != null)
                                    .ToList();
            List<byte[]> files = new List<byte[]>();

            foreach(var student in students)
            {
                files.Add(student.OtherData.Curriculum);
            }


            var oMail = new SystemSupportMail();
            try
            {

                oMail.SendEmail(
                    subject: "asuntotest",
                    body: "bodytest",
                    recipientMail: email,
                    byteFiles: files
                    );

            }
            catch (Exception ex)
            {
                var e = ex;
 
            }
            finally
            {

            }
            return Ok();
            
        }

        [HttpGet("DownloadCV")]
        [Authorize(Roles = "Empresa, Administrador")]
        public async Task<IActionResult> GetCurriculum(string userId)
        {
            var dbObject = await _userManagerStudent.FindByIdAsync(userId);
            var od = _context.OtherData.FirstOrDefault(o => o.Id == dbObject.OtherDataId);

            if (od is null)
            {
                return NotFound();
            }

            if (od.Curriculum is null)
            {
                return BadRequest();
            }

            return File(od.Curriculum, "application/pdf", $"{dbObject.Name}_CV.pdf");
        }

    }
}
