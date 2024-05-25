using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Attribute;
using BooksAPI.FE.Messages;

namespace BooksAPI.FE.Model;

public class LibraryMangaModel
{
    [Required(ErrorMessage = LibraryMangaMessages.TitleRomajiRequired)]
    public string TitleRomaji { get; set; } = string.Empty;
    
    public string? TitleEnglish { get; set; } = string.Empty;
    
    [Required(ErrorMessage = LibraryMangaMessages.TitleJapaneseRequired)]
    public string TitleJapanese { get; set; } = string.Empty;
    
    [Required(ErrorMessage = LibraryMangaMessages.DemographicRequired)]
    public string DemographicType { get; set; } = string.Empty;
   
    [Required(ErrorMessage = LibraryMangaMessages.TypeRequired)]
    public string Type { get; set; } = string.Empty;
   
    [Required(ErrorMessage = LibraryMangaMessages.PublishingStatusRequired)]
    [PublishingStatusValidation]
    public string PublishingStatus { get; set; } = string.Empty;

    [Required(ErrorMessage = LibraryMangaMessages.SynopsisRequired)]
    public string Synopsis { get; set; } = string.Empty;
    
    public string? MainImageUrl { get; set; } = string.Empty;
   
    [TotalVolumeValidation]
    public int? TotalVolumes { get; set; }
    
    [EnsureMinimumElements(1, ErrorMessage = LibraryMangaMessages.AuthorsRequired)]
    public List<AuthorModel> Authors { get; set; } = new List<AuthorModel>();
}