using dotnet_backend.Models;

namespace dotnet_backend.Services;

public class IAuthService
{
    Task<object> LoginAsync(LoginDTO dto)
    {
        return Task.FromResult(new object());
    }
}