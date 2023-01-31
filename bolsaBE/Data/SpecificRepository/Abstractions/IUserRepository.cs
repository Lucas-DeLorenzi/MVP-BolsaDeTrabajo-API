namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IUserRepository
    {
        Task<bool> GenerateForgotPasswordTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
