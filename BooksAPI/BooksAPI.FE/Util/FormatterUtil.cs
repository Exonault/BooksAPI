using BooksAPI.FE.Constants;
using BooksAPI.FE.Contracts.Author;

namespace BooksAPI.FE.Util;

public static class FormatterUtil
{
    public static string FormatTotalVolumes(int? totalVolumes)
    {
        if (totalVolumes is null)
        {
            return "(?) volumes";
        }

        return $"{totalVolumes} volumes";
    }

    public static string FormatAuthor(AuthorResponse author)
    {
        string role = "";
        if (author.Role == AuthorConstants.AuthorRole.StoryAndArt)
        {
            role = "Story & Art";
        }
        else role = author.Role;

        string result = $"{author.FirstName} {author.LastName}: {role}";
        return result;
    }
}