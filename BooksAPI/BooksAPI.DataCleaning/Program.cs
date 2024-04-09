using System.Globalization;
using System.Text.Json;
using BooksAPI.DataCleaning;
using CsvHelper;
using CsvHelper.Configuration;

//DO NOT RUN; 
return;

string filePathData = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\manga - Copy.csv";
//string filePathData = @"C:\Users\krist\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\manga - Copy.csv";
string filePathAuthorsOut = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\authors.csv";
//string filePathAuthorsOut = @"C:\Users\krist\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\authors.csv";
string filePathMangasOut = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\mangas.csv";
//string filePathMangasOut = @"C:\Users\krist\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\mangas.csv";
string filePathRelations = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\authorMangaRelation.csv";
//string filePathRelations = @"C:\Users\krist\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\authorMangaRelation.csv"";
if (File.Exists(filePathData))
{
    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
    };

    using (var reader = new StreamReader(filePathData))
    using (var csv = new CsvReader(reader, csvConfig))
    {
        List<Author> authorsForCsv = new List<Author>();
        List<LibraryComic> mangasForCsv = new List<LibraryComic>();
        // List<string> demographicsList = new List<string>();

        List<(string authorId, string libraryComicId)> authorIdLibraryComicIdForCsv = new List<(string, string)>();

        csv.Read();
        csv.ReadHeader();

        int i = 0;
        while (csv.Read())
        {
            //string mangaId = csv.GetField("manga_id");
            // string score = csv.GetField("score");
            //string scoredBy = csv.GetField("scored_by");
            //string chapters = csv.GetField("chapters");
            //string startDate = csv.GetField("start_date");
            //string endDate = csv.GetField("end_date");
            //string members = csv.GetField("members");
            //string favorites = csv.GetField("favorites");
            //string sfw = csv.GetField("sfw");
            //string approved = csv.GetField("approved");
            // string createdAtBefore = csv.GetField("created_at_before");
            // string updatedAt = csv.GetField("updated_at");
            // string realStartDate = csv.GetField("real_start_date");
            //string realEndDate = csv.GetField("real_end_date");
            //string genres = csv.GetField("genres");
            // string themes = csv.GetField("themes");
            //string synopsis = csv.GetField("synopsis");
            //string background = csv.GetField("background");
            // string mainPicture = csv.GetField("main_picture");
            //string url = csv.GetField("url");
            // string titleEnglish = csv.GetField("title_english");
            // string titleJapanese = csv.GetField("title_japanese");
            //string titleSynonyms = csv.GetField("title_synonyms");
            // Do something with the fields
            // Console.WriteLine(
            //     $"Manga ID: {mangaId}, Title: {title}, Type: {type}, Score: {score}, Scored By: {scoredBy}");
            //
            // Console.WriteLine($"{synopsis}");
            // Console.WriteLine($"{background}");

            string title = csv.GetField("title");
            string type = csv.GetField("type"); //manga; manhwa; light_novel; one_shot; manhua; novel; doujinshi; 
            string status = csv.GetField("status"); // currently_publishing; finished; on_hiatus; discontinued; 
            string volumes = csv.GetField("volumes");
            string demographics = csv.GetField("demographics");
            string authors = csv.GetField("authors");

            //Console.WriteLine($"{title}; {type}; {status}; {volumes}; {demographics}; {authors}");
            type = FormatType(type);
            status = FormatPublishingStatus(status);
            demographics = FormatDemographic(demographics);

            // if (!demographicsList.Contains(demographics))
            // {
            //     Console.WriteLine(demographics);
            //     demographicsList.Add(demographics);
            //    
            // }

            if ("other" == type || "other" == status || "other" == demographics)
            {
                continue;
            }

            int? volumesInt;
            if (int.TryParse(volumes, out int result))
            {
                volumesInt = result;
            }
            else volumesInt = null;

            List<Author> extractedAuthors = FormatAuthors(authors, title);

            //string authorsSerialized = JsonSerializer.Serialize(extractedAuthors);
            LibraryComic libraryComic = new LibraryComic
            {
                Id = Guid.NewGuid(),
                Title = title,
                DemographicType = demographics,
                ComicType = type,
                PublishingStatus = status,
                TotalVolumes = volumesInt,
                //Authors = authorsSerialized
            };

            mangasForCsv.Add(libraryComic);

            foreach (var author in extractedAuthors)
            {
                Author? authorSearch = authorsForCsv
                    .FirstOrDefault(a => a.FirstName == author.FirstName
                                         && a.LastName == author.LastName
                                         && a.Role == author.Role);

                if (authorSearch is null)
                {
                    authorsForCsv.Add(author);
                    authorIdLibraryComicIdForCsv.Add((author.Id, libraryComic.Id.ToString()));
                }
                else
                {
                    authorIdLibraryComicIdForCsv.Add((authorSearch.Id, libraryComic.Id.ToString()));
                }
            }
        }

        WriteAuthorsToCsv(authorsForCsv);
        WriteMangaToCsv(mangasForCsv);
        WriteRelationsToCsv(authorIdLibraryComicIdForCsv);

        
    }
}
else
{
    Console.WriteLine("File not found: " + filePathData);
}

string FormatType(string type)
{
    //manga; light_novel; one_shot;
    if ("manga" == type)
    {
        type = "Manga";
    }
    else if ("light_novel" == type)
    {
        type = "LightNovel";
    }
    else if ("one_shot" == type)
    {
        type = "OneShot";
    }
    else type = "other";

    return type;
}

string FormatPublishingStatus(string status)
{
    // currently_publishing; finished; on_hiatus; discontinued; 
    if ("currently_publishing" == status)
    {
        status = "Publishing";
    }
    else if ("finished" == status)
    {
        status = "Finished";
    }
    else if ("on_hiatus" == status)
    {
        status = "OnHiatus";
    }
    else status = "other";

    return status;
}

string FormatDemographic(string demographic)
{
    string[] demographicsSplit = demographic.Replace("[", "")
        .Replace("]", "")
        .Replace("\'", "")
        .Split(",", StringSplitOptions.RemoveEmptyEntries);

    if (demographicsSplit.Length == 0)
    {
        return "other";
    }
    else if (demographicsSplit.Length == 1)
    {
        if (demographicsSplit[0] == "Kids")
        {
            return "other";
        }
        else return demographicsSplit[0];
    }
    else if (demographicsSplit.Length == 2)
    {
        if (demographicsSplit[0] == "Kids")
        {
            return demographicsSplit[1];
        }

        return demographicsSplit[0];
    }

    return "other";
}

List<Author> FormatAuthors(string authors, string title)
{
    List<Author> result = new List<Author>();
    try
    {
        authors = authors.Replace("'", "\"");
        IList<AuthorCSV>? authorsDeserialized = JsonSerializer.Deserialize<IList<AuthorCSV>>(authors);
        if (authorsDeserialized is not null)
            foreach (AuthorCSV author in authorsDeserialized)
            {
                author.Role = author.Role == "Story & Art" ? "StoryAndArt" : author.Role;

                Author mappedAuthor = new Author
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = string.IsNullOrEmpty(author.FirstName) ? null : author.FirstName,
                    LastName = author.LastName,
                    Role = author.Role,
                };
                result.Add(mappedAuthor);
            }
    }
    catch (Exception e)
    {
        Console.WriteLine($"{authors}");
    }

    return result;
}

void WriteAuthorsToCsv(List<Author> authors)
{
    using (var writer = new StreamWriter(filePathAuthorsOut))
    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        csv.WriteRecords(authors);
    }
}

void WriteMangaToCsv(List<LibraryComic> libraryComics)
{
    using (var writer = new StreamWriter(filePathMangasOut))
    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        csv.WriteRecords(libraryComics);
    }
}

void WriteRelationsToCsv(List<(string authorId, string libraryComicId)> relations)
{
    using (var writer = new StreamWriter(filePathRelations))
    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        csv.Context.RegisterClassMap<RelationMap>();
        
        csv.WriteRecords(relations);
    }
}