using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Attribute;

namespace BooksAPI.FE.Model;

public class UserMangaModel
{               
    public string ReadingStatus { get; set; } = string.Empty;
    
    [ReadVolumesValidation("CollectedVolumes")]
    public int? ReadVolumes { get; set; }

    public int? CollectedVolumes { get; set; }
    
    public decimal? Price { get; set; }

    public string CollectionStatus { get; set; } = string.Empty;
}