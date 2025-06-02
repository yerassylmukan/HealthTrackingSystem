namespace WebApplication1.Common.Contracts;

public interface IAuthService
{
    Task<Result<string>> RegisterAsync(string userName, string password, string name, int age, float weight,
        float height, string goal);

    Task<Result<string>> LoginAsync(string userName, string password);
}