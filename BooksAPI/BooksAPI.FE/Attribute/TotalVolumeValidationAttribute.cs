using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Constants;

namespace BooksAPI.FE.Attribute;

public class TotalVolumeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        int valueInt = Convert.ToInt32(value);
        var property = validationContext.ObjectType.GetProperty("PublishingStatus");
        if (property == null)
        {
            return new ValidationResult($"Unknown property: PublishingStatus");
        }

        var otherValue = property.GetValue(validationContext.ObjectInstance, null);
        string? otherValueString = Convert.ToString(otherValue);

        if (LibraryMangaConstants.PublishingType.Publishing == otherValueString && valueInt != 0)
        {
            return new ValidationResult(this.ErrorMessage);
        }

        return null;
    }
}