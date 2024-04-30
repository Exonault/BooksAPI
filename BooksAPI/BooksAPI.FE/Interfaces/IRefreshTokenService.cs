namespace BooksAPI.FE.Interfaces;

public interface IRefreshTokenService
{
    public Task<bool> RefreshToken(string token, string refreshToken);

    public Task<string[]> GetTokens();
}