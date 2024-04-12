using System.ComponentModel.DataAnnotations;

namespace BooksAPI.FE.Data;

public class RegisterModel //TODO add errorMessages
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;
}