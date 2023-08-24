using System.ComponentModel.DataAnnotations;
using BookAPI.Presentation.Attributes;

namespace BookAPI.Presentation.Models;

public class ModifyComicsModel
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required")]
    [Range(1,double.MaxValue,ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Reading status is required")]
    public string ReadingStatus { get; set; } = string.Empty;

    [Required(ErrorMessage = "Demographic type is required")]
    public string DemographicType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Comic type is required")]
    public string ComicType { get; set; } = string.Empty;

    [Required(ErrorMessage = "Publishing status is required")]
    public string PublishingStatus { get; set; } = string.Empty;

    [Required(ErrorMessage = "Total volumes is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Total volumes must be at least 1")]
    public int TotalVolumes { get; set; }

    [Required(ErrorMessage = "Collected volumes is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Collected volumes must be positive")]
    [LessThanOrEqualToTotalVolumesValidator("TotalVolumes")]
    public int CollectedVolumes { get; set; }
}