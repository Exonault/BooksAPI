using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Model;

namespace BooksAPI.FE.Attribute;

public class ReadVolumesValidationAttribute : ValidationAttribute
{
    private readonly string _other;

    public ReadVolumesValidationAttribute(string other)
    {
        _other = other;
    }

    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var userMangaModel = (UserMangaModel)validationContext.ObjectInstance;

        int readVolumes = userMangaModel.ReadVolumes;
        int collectedVolumes = userMangaModel.CollectedVolumes;

        if (readVolumes > collectedVolumes)
        {
            return new ValidationResult("Read volumes must be less than or equal to collected volumes");
        }
        
        return ValidationResult.Success;
    }
}