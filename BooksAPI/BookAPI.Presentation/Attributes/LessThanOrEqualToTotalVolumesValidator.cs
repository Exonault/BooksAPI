using System.ComponentModel.DataAnnotations;

namespace BookAPI.Presentation.Attributes;

public class LessThanOrEqualToTotalVolumesValidator : ValidationAttribute
{
    private readonly string _other;

    public LessThanOrEqualToTotalVolumesValidator(string other)
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
            return new ValidationResult("Collected volumes must be less than or equal to total volumes");
        }

        return null;
    }
}