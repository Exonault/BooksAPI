using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Attribute;
using BooksAPI.FE.Messages;

namespace BooksAPI.FE.Model;

public class LibraryMangaModel
{
    [Required(ErrorMessage = LibraryMangaMessages.TitleRequired)]
    [StringLength(100, ErrorMessage = LibraryMangaMessages.TitleLengthMessage, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = LibraryMangaMessages.DemographicRequired)]
    public string DemographicType { get; set; } = string.Empty;
   
    [Required(ErrorMessage = LibraryMangaMessages.TypeRequired)]
    public string Type { get; set; } = string.Empty;
   
    [Required(ErrorMessage = LibraryMangaMessages.PublishingStatusRequired)]
    public string PublishingStatus { get; set; } = string.Empty;
    
    public string? MainImageUrl { get; set; } = string.Empty;
   
    [TotalVolumeValidation(ErrorMessage = LibraryMangaMessages.TotalVolumesPublishingStatusMessage)]
    public int? TotalVolumes { get; set; }
    
    [EnsureMinimumElements(1, ErrorMessage = LibraryMangaMessages.AuthorsRequired)]
    public List<AuthorModel> Authors { get; set; } = new List<AuthorModel>();
}