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


    public static string FormatCollectionStatus(string collectionStatus)
    {
        if (collectionStatus == UserMangaConstants.CollectingStatus.InProgress)
        {
            return "In progress";
        }
        else if (collectionStatus == UserMangaConstants.CollectingStatus.PlanToCollect)
        {
            return "Plan to collect";
        }
        else return collectionStatus;
    }

    public static string FormatReadingStatus(string readingStatus)
    {
        if (readingStatus == UserMangaConstants.ReadingStatus.OnHold)
        {
            return "On hold";
        }
        else if (readingStatus == UserMangaConstants.ReadingStatus.PlanToRead)
        {
            return "Plan to read";
        }
        else return readingStatus;
    }
}