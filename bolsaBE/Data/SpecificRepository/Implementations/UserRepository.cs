using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Entities.Users.Implementations;
using bolsaBE.Models.Users;
using bolsaBE.Models.Users.Student;
using bolsaBE.Services.MailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly BolsaDeTrabajoContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly ISystemSupportMail _systemSupportMail;

        public UserRepository(BolsaDeTrabajoContext context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ISystemSupportMail systemSupportMail)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
            _systemSupportMail = systemSupportMail;
        }

        public async Task<bool> GenerateForgotPasswordTokenAsync(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            if (!string.IsNullOrEmpty(token))
            {
                string sbj = "Código de recuperación de contraseña - Bolsa de trabajo UTN";
                string body = $"El código de recuperación es {token}";
                _systemSupportMail.SendEmail(sbj, body, user.Email);
                return true;
            }

            return false;
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword )
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                return false;
            }

            return _userManager.ResetPasswordAsync(user, token, newPassword).Result.Succeeded;
        }
    }
}
