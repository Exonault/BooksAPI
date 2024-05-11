namespace BooksAPI.FE.Constants;

public static class LibraryMangaConstants
{
    public static class DemographicType
    {
        public const string Shounen = "Shounen";
        public const string Seinen = "Seinen";
        public const string Shoujo = "Shoujo";
        public const string Josei = "Josei";

        public static readonly IReadOnlyList<string> DemographicTypes = new[]
        {
            Shounen,
            Seinen,
            Shoujo,
            Josei,
        };

        private static Dictionary<string, string> tempDictionary = new Dictionary<string, string>()
        {
            { Shounen, "Shounen" },
            { Seinen, "Seinen" },
            { Shoujo, "Shoujo" },
            { Josei, "Josei" },
        };

        // public static readonly IReadOnlyDictionary<string, string> DemographicTypesWithPresentation =
        //     new ReadOnlyDictionary<string, string>(tempDictionary);

        public static string GetKeyByValue(string value)
        {
            return tempDictionary.FirstOrDefault(x => x.Value == value).Key;
        }

        public static string GetValueByKey(string key)
        {
            return tempDictionary.FirstOrDefault(x => x.Key == key).Value;
        }
    }

    public static class Type
    {
        public const string Manga = "Manga";
        public const string LightNovel = "LightNovel";
        public const string OneShot = "OneShot";

        public static readonly IReadOnlyList<string> ComicTypes = new List<string>
        {
            Manga,
            LightNovel,
            OneShot
        };

        private static Dictionary<string, string> tempDictionary = new Dictionary<string, string>()
        {
            { Manga, "Manga" },
            { LightNovel, "Light novel" },
            { OneShot, "One shot" },
        };

        // public static readonly IReadOnlyDictionary<string, string> TypesWithPresentation =
        //     new ReadOnlyDictionary<string, string>(tempDictionary);

        public static string GetKeyByValue(string value)
        {
            return tempDictionary.FirstOrDefault(x => x.Value == value).Key;
        }

        public static string GetValueByKey(string key)
        {
            return tempDictionary.FirstOrDefault(x => x.Key == key).Value;
        }
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

        private static Dictionary<string, string> tempDictionary = new Dictionary<string, string>()
        {
            { Publishing, "Publishing" },
            { Finished, "Finished" },
            { OnHiatus, "On Hiatus" },
        };

        // public static readonly IReadOnlyDictionary<string, string> PublishingStatusWithPresentation =
        //     new ReadOnlyDictionary<string, string>(tempDictionary);

        public static string GetKeyByValue(string value)
        {
            return tempDictionary.FirstOrDefault(x => x.Value == value).Key;
        }

        public static string GetValueByKey(string key)
        {
            return tempDictionary.FirstOrDefault(x => x.Key == key).Value;
        }
    }
}