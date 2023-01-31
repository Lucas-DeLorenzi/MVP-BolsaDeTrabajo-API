namespace bolsaBE.Services.Abstractions
{
    public interface IUserServices
    {
        Task<bool> GenerateForgotPasswordTokenAsync(string email);

        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
