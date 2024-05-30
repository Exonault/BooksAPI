using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Constants;
using BooksAPI.FE.Messages;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Attribute;

public class TotalVolumeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        LibraryMangaModel model = (LibraryMangaModel)validationContext.ObjectInstance;

        string publishingStatus = model.PublishingStatus;
        string type = model.Type;
        int? totalVolumes = model.TotalVolumes;

        if (LibraryMangaConstants.PublishingType.Publishing == publishingStatus && totalVolumes is not null)
        {
            return new ValidationResult(LibraryMangaMessages.TotalVolumesPublishingStatusMessage);
        }

        if (LibraryMangaConstants.Type.OneShot == type && totalVolumes is not 1)
        {
            return new ValidationResult(LibraryMangaMessages.TotalVolumesOneShot);
        }

        return ValidationResult.Success;
    }
}