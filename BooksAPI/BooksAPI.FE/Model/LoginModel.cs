using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Messages;

namespace BooksAPI.FE.Model;

public class LoginModel
{
    [Required]
    [StringLength(50, ErrorMessage = UserMessages.UsernameErrorMessage , MinimumLength = 8)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}