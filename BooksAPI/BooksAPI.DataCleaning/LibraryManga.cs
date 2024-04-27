namespace BooksAPI.DataCleaning;

public class LibraryManga
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DemographicType { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string PublishingStatus { get; set; } = string.Empty;
    public int? TotalVolumes { get; set; }

    public string MainImageUrl { get; set; }
    
   //public List<Author> Authors { get; set; }
   // public string Authors { get; set; }
}