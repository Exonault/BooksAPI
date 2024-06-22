using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Constants;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Attribute;

public class CollectionStatusValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userMangaModel = (UserMangaModel)validationContext.ObjectInstance;

        string collectionStatus = userMangaModel.CollectionStatus;
        int collectedVolumes = userMangaModel.CollectedVolumes;
        var libraryManga = userMangaModel.LibraryMangaResponse;

        if (collectionStatus == UserMangaConstants.CollectingStatus.Collected &&
            libraryManga!.PublishingStatus == LibraryMangaConstants.PublishingType.Publishing)
        {
            return new ValidationResult("You can't have a collected manga that is still publishing");
        }

        if (collectionStatus == UserMangaConstants.CollectingStatus.Collected &&
            libraryManga!.TotalVolumes is not null &&
            collectedVolumes > libraryManga.TotalVolumes)
        {
            return new ValidationResult("Collected volumes must be less than or equal to the total volumes");
        }

        return ValidationResult.Success;
    }
}