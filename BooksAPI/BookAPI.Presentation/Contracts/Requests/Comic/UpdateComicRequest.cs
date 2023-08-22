namespace BookAPI.Presentation.Contracts.Requests.Comic;

public class UpdateComicRequest
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string ReadingStatus { get; set; } = string.Empty;
    
    public string DemographicType { get; set; } = string.Empty;

    public string ComicType { get; set; } = string.Empty;

    public string PublishingStatus { get; set; } = string.Empty;
    
    public int TotalVolumes { get; set; }

    public int CollectedVolumes { get; set; }
}