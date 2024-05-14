using BooksAPI.FE.Contracts.LibraryManga;

namespace BooksAPI.FE.Model;

public class UserMangaModel
{               
    public string ReadingStatus { get; set; } = string.Empty;
    
    public int ReadVolumes { get; set; }

    public int CollectedVolumes { get; set; }
    
    public decimal Price { get; set; }

    public string CollectionStatus { get; set; } = string.Empty;

    public LibraryMangaResponse LibraryManga { get; set; }
}