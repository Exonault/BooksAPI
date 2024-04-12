using System.ComponentModel.DataAnnotations;

namespace BooksAPI.FE.Contracts.User;

public class RegisterRequest
{
    
    [Required]
    public string UserName { get; set; } = string.Empty;
    
    
    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool Admin { get; set; } = false;
}