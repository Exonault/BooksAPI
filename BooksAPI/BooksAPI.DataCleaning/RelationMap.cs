using CsvHelper.Configuration;

namespace BooksAPI.DataCleaning;

public sealed class RelationMap:ClassMap<(int authorId, int libraryMangaId)>
{
    public RelationMap()
    {
        Map(m => m.authorId).Name("AuthorsId");
        Map(m => m.libraryMangaId).Name("LibraryMangasId");
    }
}