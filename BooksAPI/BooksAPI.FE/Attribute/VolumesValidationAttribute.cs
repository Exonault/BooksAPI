using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Attribute;

public class VolumesValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var userMangaModel = (UserMangaModel)validationContext.ObjectInstance;
        

        return base.IsValid(value, validationContext);
    }
}