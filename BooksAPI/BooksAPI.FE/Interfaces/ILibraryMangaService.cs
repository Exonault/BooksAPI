using BooksAPI.FE.Contracts.LibraryManga;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Interfaces;

public interface ILibraryMangaService
{
    Task<IEnumerable<LibraryMangaResponse>> GetMangasForPage(int page, int entries);

    Task<LibraryMangaModel> GetMangaModel(int id);

    Task<LibraryMangaResponse?> GetManga(int id);

    Task<IEnumerable<LibraryMangaResponse>> SearchManga(string search);

    Task<bool> CreateManga(LibraryMangaModel model, string token, string refreshToken);

    Task<bool> UpdateManga(int id, LibraryMangaModel model, string token, string refreshToken);

    Task<bool> DeleteManga(int id, string token, string refreshToken);
}