namespace BooksAPI.Entities;

public class Comic:Book
{
    public string DemographicType { get; set; } = string.Empty;

    public string ComicType { get; set; } = string.Empty;

    public string PublishingStatus { get; set; } = string.Empty;

    public int TotalVolumes { get; set; }

    public int CollectedVolumes { get; set; }
}