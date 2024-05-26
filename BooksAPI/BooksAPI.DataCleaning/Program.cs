using System.Globalization;
using System.Text.Json;
using BooksAPI.DataCleaning;
using CsvHelper;
using CsvHelper.Configuration;

//DO NOT RUN; 
//return;

// string filePathData = @"C:\Users\krist\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\CSV\manga - Copy.csv";
// string filePathAuthorsOut = @"C:\Users\krist\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\CSV\authors.csv";
// string filePathMangasOut = @"C:\Users\krist\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\CSV\mangasWithSynopsis.csv";
// string filePathRelations = @"C:\Users\krist\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\CSV\authorMangaRelation.csv";

string filePathData = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\CSV\manga - Copy.csv";
string filePathAuthorsOut = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\CSV\authors.csv";
string filePathMangasOut = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\CSV\mangas.csv";
string filePathRelations = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\CSV\authorMangaRelation.csv";

if (File.Exists(filePathData))
{
    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
    };

    using (var reader = new StreamReader(filePathData))
    using (var csv = new CsvReader(reader, csvConfig))
    {
        csv.Read();
        csv.ReadHeader();
        
        List<Author> authorsForCsv = new List<Author>();
        List<LibraryManga> mangasForCsv = new List<LibraryManga>();

        List<(int authorId, int libraryMangaId)> authorLibraryMangaRelation = new List<(int, int)>();
        
        int mangaId = 1;
        int authorId = 1;
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
            //string background = csv.GetField("background");
           
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

            string titleRomaji = csv.GetField("title");
            string titleEnglish = csv.GetField("title_english");
            string titleJapanese = csv.GetField("title_japanese");
            string type = csv.GetField("type");                                     //manga; manhwa; light_novel; one_shot; manhua; novel; doujinshi; 
            string status = csv.GetField("status");                                  // currently_publishing; finished; on_hiatus; discontinued; 
            string volumes = csv.GetField("volumes");
            string demographics = csv.GetField("demographics");
            string authors = csv.GetField("authors");
            string mainPicture = csv.GetField("main_picture");
            string synopsis = csv.GetField("synopsis");
            
            string[] temp = synopsis.Split("[Written by MAL Rewrite]", StringSplitOptions.TrimEntries);
            
            synopsis = temp[0];
            type = FormatType(type);
            status = FormatPublishingStatus(status);
            demographics = FormatDemographic(demographics);

            if ("other" == type || "other" == status || "other" == demographics)
            {
                continue;
            }

            int? totalVolumes;
            if (int.TryParse(volumes, out int result))
            {
                totalVolumes = result;
            }
            else totalVolumes = null;

            List<Author> extractedAuthors = FormatAuthors(authors, titleRomaji);

            //string authorsSerialized = JsonSerializer.Serialize(extractedAuthors);
            LibraryManga libraryManga = new LibraryManga
            {
                Id = mangaId,
                TitleRomaji = titleRomaji,
                TitleEnglish = titleEnglish,
                TitleJapanese = titleJapanese,
                DemographicType = demographics,
                Type = type,
                PublishingStatus = status,
                TotalVolumes = totalVolumes,
                MainImageUrl = mainPicture,
                Synopsis = synopsis,
            };

            mangasForCsv.Add(libraryManga);

            foreach (Author author in extractedAuthors)
            {
                Author? authorSearch = authorsForCsv
                    .FirstOrDefault(a => a.FirstName == author.FirstName
                                         && a.LastName == author.LastName
                                         && a.Role == author.Role);

                if (authorSearch is null)
                {
                    author.Id = authorId;
                    authorsForCsv.Add(author);
                    authorLibraryMangaRelation.Add((author.Id, libraryManga.Id));
                    authorId++;
                }
                else
                {
                    authorLibraryMangaRelation.Add((authorSearch.Id, libraryManga.Id));
                }
            }

            mangaId++;
        }

        WriteAuthorsToCsv(authorsForCsv);
        WriteMangaToCsv(mangasForCsv);
        WriteRelationsToCsv(authorLibraryMangaRelation);
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

void WriteMangaToCsv(List<LibraryManga> libraryComics)
{
    using (var writer = new StreamWriter(filePathMangasOut))
    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        csv.WriteRecords(libraryComics);
    }
}

void WriteRelationsToCsv(List<(int authorId, int libraryMangaId)> relations)
{
    using (var writer = new StreamWriter(filePathRelations))
    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        csv.Context.RegisterClassMap<RelationMap>();

        csv.WriteRecords(relations);
    }
}