using System.ComponentModel.DataAnnotations;

namespace BooksAPI.BE.Contracts.User;

public class LoginRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}