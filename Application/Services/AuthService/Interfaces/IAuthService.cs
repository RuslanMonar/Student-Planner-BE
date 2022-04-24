using Application.Results;

namespace Application.Services.AuthService.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(string email, string password, string username);
    }
}
