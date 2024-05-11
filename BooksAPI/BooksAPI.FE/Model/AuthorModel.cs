using System.ComponentModel.DataAnnotations;
using BooksAPI.FE.Messages;

namespace BooksAPI.FE.Model;

public class AuthorModel
{
    public string? FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = AuthorMessages.LastNameRequired)]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = AuthorMessages.RoleRequired)]
    public string Role { get; set; } = string.Empty;
}