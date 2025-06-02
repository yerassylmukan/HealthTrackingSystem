namespace WebApplication1.Common.Contracts;

public interface ITokenClaimService
{
    Task<Result<string>> GetTokenAsync(string userName);
}