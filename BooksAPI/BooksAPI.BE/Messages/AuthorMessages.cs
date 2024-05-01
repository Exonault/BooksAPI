using BooksAPI.BE.Constants;

namespace BooksAPI.BE.Messages;

public class AuthorMessages
{
    public const string FirstNameRequired = "First name is required";
    public const string LastNameRequired = "Last name is required";
    public const string RoleRequired = "Role is required";
    public static readonly string RoleMessage =
        $"Role must be one of the following: {string.Join(" ", AuthorConstants.AuthorRole.AuthorRoles)}";
}