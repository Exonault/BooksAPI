using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Constants;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Attribute;

public class PublishingStatusValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        LibraryMangaModel model = (LibraryMangaModel)validationContext.ObjectInstance;

        string publishingStatus = model.PublishingStatus;
        string type = model.Type;


        if (LibraryMangaConstants.Type.OneShot == type && publishingStatus is LibraryMangaConstants.PublishingType.Publishing or LibraryMangaConstants.PublishingType.OnHiatus)
        {
            return new ValidationResult(
                $"Publishing status cannot be {LibraryMangaConstants.PublishingType.GetLabelByKey(publishingStatus)} when the type is One shot");
        }
        
        return ValidationResult.Success;
    }
}