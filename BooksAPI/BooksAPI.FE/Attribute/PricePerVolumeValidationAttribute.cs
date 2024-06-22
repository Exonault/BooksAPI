using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Constants;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Attribute;

public class PricePerVolumeValidationAttribute:ValidationAttribute
{
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userMangaModel = (UserMangaModel)validationContext.ObjectInstance;

        decimal pricePerVolume = userMangaModel.PricePerVolume;
        string collectionStatus = userMangaModel.CollectionStatus;


        if ((collectionStatus is UserMangaConstants.CollectingStatus.InProgress or UserMangaConstants.CollectingStatus.Collected)
            && pricePerVolume == Decimal.Zero)
        {
            return new ValidationResult($"Price can't be zero when status is {collectionStatus}");
        }
        
        return ValidationResult.Success;

        // string collectionStatus = userMangaModel.CollectionStatus;
        // int collectedVolumes = userMangaModel.CollectedVolumes;
        // var libraryManga = userMangaModel.LibraryMangaResponse;
        //
        // if (collectionStatus == UserMangaConstants.CollectingStatus.Collected &&
        //     libraryManga!.PublishingStatus == LibraryMangaConstants.PublishingType.Publishing)
        // {
        //     return new ValidationResult("Cannot collect a manga that is still publishing.");
        // }
        //
        // if (collectionStatus == UserMangaConstants.CollectingStatus.Collected &&
        //     libraryManga!.TotalVolumes is not null &&
        //     collectedVolumes > libraryManga.TotalVolumes)
        // {
        //     return new ValidationResult("Collected volumes must be less than or equal to the total volumes");
        // }
        //
        // return ValidationResult.Success;
    }
    
}