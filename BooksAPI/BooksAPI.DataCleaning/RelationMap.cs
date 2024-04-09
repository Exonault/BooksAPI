using CsvHelper.Configuration;

namespace BooksAPI.DataCleaning;

public sealed class RelationMap:ClassMap<(string authorId, string libraryComicId)>
{
    public RelationMap()
    {
        Map(m => m.authorId).Name("authorId");
        Map(m => m.libraryComicId).Name("libraryComicId");
    }
}