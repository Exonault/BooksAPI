using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Entities;


[Index(nameof(Id))]
public class LibraryManga
{
    public int Id { get; set; }

    public string TitleRomaji { get; set; } = string.Empty; 
    
    public string? TitleEnglish { get; set; }
    
    public string TitleJapanese { get; set; } = string.Empty; 
    
    public string DemographicType { get; set; } = string.Empty;
    
    public string Type { get; set; } = string.Empty;
    
    public string PublishingStatus { get; set; } = string.Empty;
    
    public int? TotalVolumes { get; set; }
    
    public string? MainImageUrl { get; set; }
    
    public string Synopsis { get; set; } = string.Empty;
    
    public List<UserManga> UserMangas { get; set; }
    
    public List<Author> Authors { get; set; }
}