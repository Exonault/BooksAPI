using System.ComponentModel.DataAnnotations;

namespace BooksAPI.BE.Contracts.User;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool Admin { get; set; }
}