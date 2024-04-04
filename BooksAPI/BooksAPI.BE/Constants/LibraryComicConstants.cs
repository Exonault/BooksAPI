namespace BooksAPI.BE.Constants;

public class LibraryComicConstants
{
    public static class DemographicType
    {
        public const string Shonen = "Shonen";
        public const string Seinen = "Seinen";
        public const string Shojo = "Shojo";
        public const string Josei = "Josei";
        public const string Kodomomuke = "Kodomomuke";
        
        public static readonly IReadOnlyList<string> DemographicTypes = new[]
        {
            Shonen,
            Seinen,
            Shojo,
            Josei,
            Kodomomuke,
        };
        
    }
    
    public static class ComicType
    {
        public const string Comic = "Comic";
        public const string Manga = "Manga";
        public const string LightNovel = "LightNovel";
        public const string OneShot = "OneShot";
        
        public static readonly IReadOnlyList<string> ComicTypes = new List<string>
        {
            Comic,
            Manga,
            LightNovel,
            OneShot
        };
    }
    
    public static class PublishingType
    {
        public const string Publishing = "Publishing";
        public const string Finished = "Finished";
        public const string OnHiatus = "OnHiatus";
        
       

        public static readonly IReadOnlyList<string> PublishingStatuses = new List<string>
        {
            Publishing,
            Finished,
            OnHiatus
        };
    }
    
}