using bolsaBE.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bolsaBE.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/login")]
    public class LogInController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserServices _userServices;

        public LogInController(IHttpContextAccessor contextAccessor, IUserServices userServices)
        {
            _contextAccessor = contextAccessor;
            _userServices = userServices;
        }

        [HttpGet]
        public ActionResult<string> GetUser()
        {
            var user = _contextAccessor.HttpContext.User;
            
            var response = new
            {
                Claims = user.Claims.Select(u => new
                {
                    u.Type,
                    u.Value,
                }).ToList(),
                user.Identity.IsAuthenticated,
                user.Identity.AuthenticationType
            };

            return Ok(response);
        }

        [HttpGet("forgotpassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(string email)
        {
            return Ok(_userServices.GenerateForgotPasswordTokenAsync(email));
           
        }

        [HttpPut("newpassword")]
        [AllowAnonymous]
        public IActionResult NewPassword(Models.Users.ForgotPasswordDTO forgotPasswordDTO)
        {
            var result = _userServices.ResetPasswordAsync(forgotPasswordDTO.Email, forgotPasswordDTO.Code, forgotPasswordDTO.Password).Result;
            if (result)
            {
                return Ok("La contraseña fue reestablecida con exito");
            }
            else
            {
                return BadRequest("Ocurrió un error al intentar reestablecer la contraseña");   
            }
            
        }


    }
}
