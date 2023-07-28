namespace BooksAPI.Contracts.Response.Comic;

public class BaseComicResponse:BookBaseResponse
{
    public string DemographicType { get; set; } = string.Empty;

    public string ComicType { get; set; } = string.Empty;

    public string PublishingStatus { get; set; } = string.Empty;

    public int TotalVolumes { get; set; }

    public int CollectedVolumes { get; set; }
}