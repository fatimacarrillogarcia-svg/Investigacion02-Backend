using HackerRank1.DTO;

namespace HackerRank1.Services;

public interface IAuthenticationService
{
    Task<LoginUser?> AuthenticateAsync(string email, string password);
}

public class AuthenticationService : IAuthenticationService
{
    public Task<LoginUser?> AuthenticateAsync(string email, string password)
    {
        if (email == "admin" && password == "1234")
        {
            return Task.FromResult<LoginUser?>(new LoginUser
            {
                Email = email,
                Password = password,
                Role = "admin",
            });
        }

        return Task.FromResult<LoginUser?>(null);
    }
}
