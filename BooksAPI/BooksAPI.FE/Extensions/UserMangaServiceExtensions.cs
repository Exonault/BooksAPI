using BooksAPI.FE.Contracts.UserManga;
using BooksAPI.FE.Interfaces;

namespace BooksAPI.FE.Extensions;

public static class UserMangaServiceExtensions
{
    public static async Task<List<int>> GetLibraryMangaIdsFromUserId(this IUserMangaService userMangaService,string token, string refreshToken,
        string id)
    {
        List<UserMangaResponse> userMangas = await userMangaService.GetUserMangas(token, refreshToken, id);

        List<int> result = new List<int>();

        foreach (var userMangaResponse in userMangas)
        {
            result.Add(userMangaResponse.LibraryMangaResponse.Id);
        }

        return result;
    }
}