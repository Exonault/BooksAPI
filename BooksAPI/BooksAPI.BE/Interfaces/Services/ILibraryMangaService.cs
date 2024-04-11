using BooksAPI.BE.Contracts.LibraryComic;

namespace BooksAPI.BE.Interfaces.Services;

public interface ILibraryMangaService
{
    public Task CreateLibraryManga(CreateLibraryMangaRequest request);

    public Task<LibraryMangaResponse> GetLibraryManga(Guid id);

    public Task<List<LibraryMangaResponse>> GetAllLibraryMangas();

    public Task<List<LibraryMangaResponse>> GetLibraryMangasForPage(int pageIndex, int pageEntriesCount);

    public Task UpdateLibraryManga(Guid id, UpdateLibraryMangaRequest request);
    
    public Task DeleteLibraryManga(Guid id);
}