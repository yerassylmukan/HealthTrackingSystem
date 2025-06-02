using WebApplication1.Models;

namespace WebApplication1.Common.Contracts;

public interface IAuthService
{
    Task<Result<string>> RegisterAsync(UserRegisterModel model);

    Task<Result<string>> LoginAsync(UserLoginModel model);
}