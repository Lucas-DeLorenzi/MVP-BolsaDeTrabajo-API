using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace bolsaBE.Controllers
{
    
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly UserManager<Admin> _userManagerAdmin;
        private readonly UserManager<Student> _userManagerStudent;
        private readonly UserManager<Company> _userManagerCompany;
        private readonly BolsaDeTrabajoContext _context;

        public class AuthenticationRequestBody
        {
            [DefaultValue("administrador@email.com")]
            public string? Email { get; set; }
            [DefaultValue("123qwe")]
            public string? Password { get; set; }
        }
        public AuthenticationController(
            IConfiguration config, 
            UserManager<User> userManager,
            UserManager<Admin> userManagerAdmin,
            UserManager<Student> userManagerStudent,
            UserManager<Company> userManagerCompany,
            BolsaDeTrabajoContext context
            )
        {
            _config = config;
            _userManager = userManager;
            _userManagerAdmin = userManagerAdmin;
            _userManagerStudent = userManagerStudent;
            _userManagerCompany = userManagerCompany;
            _context = context;

        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> AuthenticateAsync(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = await _userManager.FindByEmailAsync(authenticationRequestBody.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, authenticationRequestBody.Password))
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim("username", user.UserName),
                new Claim("email", $"{user.Email}")
            };

            if (roles.Any("Administrador".Contains))
            {
                var admin = await _userManagerAdmin.FindByIdAsync(user.Id.ToString());
                claims.Add(new Claim("name", $"{admin.Name}"));

            } else if (roles.Any("Alumno".Contains))
            {
                var student = await _userManagerStudent.FindByIdAsync(user.Id.ToString());
                claims.Add(new Claim("name", $"{student.Name}"));
                var validation = _context.Validations.FirstOrDefault(v => v.Id == student.ValidationId);
                bool valid = validation.UpdatedAt is not null;
                claims.Add(new Claim("validated", $"{valid}"));
            } else
            {
                var company = await _userManagerCompany.FindByIdAsync(user.Id.ToString());
                claims.Add(new Claim("name", $"{company.Name}"));
                var validation = _context.Validations.FirstOrDefault(v => v.Id == company.ValidationId);
                bool valid = validation.UpdatedAt is not null;
                claims.Add(new Claim("validated", $"{valid}"));

            }



            foreach (var role in roles)
            {
                claims.Add(new Claim("assigned_role", role));
            }
            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config["Authentication:Issuer"],
                audience: _config["Authentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return Ok(jwt);

        }


    }
}
