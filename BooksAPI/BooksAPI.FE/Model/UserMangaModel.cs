using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Attribute;
using BooksAPI.FE.Contracts.LibraryManga;
using BooksAPI.FE.Messages;

namespace BooksAPI.FE.Model;

public class UserMangaModel
{               
    [Required(ErrorMessage = UserMangaMessages.ReadingStatusRequired)]
    public string ReadingStatus { get; set; } = string.Empty;
    
    [Required(ErrorMessage = UserMangaMessages.CollectionStatusRequired)]
    [CollectionStatusValidation]
    public string CollectionStatus { get; set; } = string.Empty;
    
    [Required(ErrorMessage = UserMangaMessages.ReadVolumesRequired)]
    [ReadVolumesValidation("CollectedVolumes")]
    public int ReadVolumes { get; set; }
    
    [Required(ErrorMessage = UserMangaMessages.CollectedVolumesRequired)]
    public int CollectedVolumes { get; set; }
    
    [Required(ErrorMessage = UserMangaMessages.PriceRequired)]
    public decimal PricePerVolume { get; set; }
    
    public int LibraryMangaId { get; set; }

    public LibraryMangaResponse? LibraryMangaResponse { get; set; }

    
}