using BooksAPI.BE.Constants;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Messages;
using FluentValidation;

namespace BooksAPI.BE.Validation;

public class AuthorValidator:AbstractValidator<Author>
{
    public AuthorValidator()
    {
        // RuleFor(x => x.FirstName)
        //     .NotEmpty()
        //     .WithMessage(AuthorMessages.FirstNameRequired);
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(AuthorMessages.LastNameRequired);
        
        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage(AuthorMessages.RoleRequired)
            .Must(x=> AuthorConstants.AuthorRole.AuthorRoles.Contains(x))
            .WithMessage(AuthorMessages.RoleMessage);
    }
}