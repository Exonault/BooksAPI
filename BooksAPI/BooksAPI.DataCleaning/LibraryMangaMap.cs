using CsvHelper.Configuration;

namespace BooksAPI.DataCleaning;

public sealed class LibraryMangaMap:ClassMap<LibraryManga>
{
    public LibraryMangaMap()
    {
        Map(m => m.Id);
        Map(m => m.Title);
        Map(m => m.DemographicType);
        Map(m => m.Type);
        Map(m => m.PublishingStatus);
        Map(m => m.TotalVolumes);
        

    } 
}