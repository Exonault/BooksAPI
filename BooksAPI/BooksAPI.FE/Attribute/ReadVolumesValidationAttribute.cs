using System.ComponentModel.DataAnnotations;

namespace BooksAPI.FE.Attribute;

public class ReadVolumesValidationAttribute : ValidationAttribute
{
    private readonly string _other;

    public ReadVolumesValidationAttribute(string other)
    {
        _other = other;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        int valueInt = Convert.ToInt32(value);
        var property = validationContext.ObjectType.GetProperty(_other);
        if (property == null)
        {
            return new ValidationResult($"Unknown property: {_other}");
        }

        var otherValue = property.GetValue(validationContext.ObjectInstance, null);
        int otherValueInt = Convert.ToInt32(otherValue);

        if (valueInt >= otherValueInt)
        {
            // here we are verifying whether the 2 values are equal
            // but you could do any custom validation you like
            return new ValidationResult("Read volumes must be less than or equal to collected volumes");
        }

        return null;
    }
}