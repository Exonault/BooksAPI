using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Constants;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Attribute;

public class CollectionStatusValidationAttribute:ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userMangaModel = (UserMangaModel)validationContext.ObjectInstance;
        
        string collectionStatus = userMangaModel.CollectionStatus;
        var libraryManga = userMangaModel.LibraryMangaResponse;
        
        if (collectionStatus == UserMangaConstants.CollectingStatus.Collected && 
            libraryManga?.PublishingStatus == LibraryMangaConstants.PublishingType.Publishing)
        {
            return new ValidationResult("Cannot collect a manga that is still publishing.");
        }
        
        return ValidationResult.Success;
    }
}