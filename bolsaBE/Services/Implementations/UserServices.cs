using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.Services.Abstractions;

namespace bolsaBE.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public Task<bool> GenerateForgotPasswordTokenAsync(string email)
        {
            return _userRepository.GenerateForgotPasswordTokenAsync(email);

        }

        public Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            return _userRepository.ResetPasswordAsync(email, token, newPassword);
        }
    }
}
