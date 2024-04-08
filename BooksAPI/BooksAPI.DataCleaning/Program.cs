using System.Globalization;
using System.Text.Json;
using BooksAPI.DataCleaning;
using CsvHelper;
using CsvHelper.Configuration;

Console.WriteLine("Hello, World!");

string filePathData = @"C:\Users\krist\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\manga - Copy.csv";
string filePathAuthorsOut = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\authors.csv";
string filePathMangasOut = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\mangas.csv";
if (File.Exists(filePathData))
{
    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
    };

    // Open the file for reading
    using (var reader = new StreamReader(filePathData))
    using (var csv = new CsvReader(reader, csvConfig))
    {
        List<Author> authorsForCsv = new List<Author>();
        List<LibraryComic> mangasForCsv = new List<LibraryComic>();
        List<string> demographicsList = new List<string>();

        // Read the CSV records
        csv.Read();
        csv.ReadHeader();

        int i = 0;
        // Iterate over each record
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

            //ExtractAuthors(authors, title);

            //Console.WriteLine($"{title}; {type}; {status}; {volumes}; {demographics}; {authors}");
            type = FormatType(type);
            status = FormatPublishingStatus(status);
            string demographic = FormatDemographic(demographics);

            if (!demographicsList.Contains(demographics))
            {
               // Console.WriteLine(demographics);
                demographicsList.Add(demographics);
               
            }

            if ("other" == type || "other" == status || "other" == demographic)
            {
                continue;
            }

            int? volumesInt;
            if (int.TryParse(volumes, out int result))
            {
                volumesInt = result;
            }
            else volumesInt = null;

            LibraryComic libraryComic = new LibraryComic
            {
                Id = Guid.NewGuid(),
                Title = title,
                DemographicType = demographics,
                ComicType = type,
                PublishingStatus = status,
                TotalVolumes = volumesInt,
            };
            
            Console.WriteLine($"{i}: {title}; {type}; {status}; {volumes}; {demographic};");

            i++;
            if (i == 500) break;



            //List<Author> extractedAuthors = ExtractAuthors(authors, title);

            //mangasForCsv.Add(libraryComic);

            // foreach (var author in extractedAuthors)
            // {
            //     Author? authorSearch = authorsForCsv
            //         .FirstOrDefault(a => a.FirstName == author.FirstName
            //                              && a.LastName == author.LastName
            //                              && a.Role == author.Role);
            //
            //     if (authorSearch is null)
            //     {
            //         authorsForCsv.Add(author);
            //     }
            // }
        }

        //WriteAuthorsToCsv(authorsForCsv);
        //WriteMangaToCsv(mangasForCsv);
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
        .Replace("]","")
        .Replace("\'","")
        .Split(",", StringSplitOptions.RemoveEmptyEntries);

    //Console.WriteLine(string.Join(" ", demographicsSplit));

    if (demographicsSplit.Length == 0)
    {  
        //Console.WriteLine($"0: {string.Join(" ", demographicsSplit)}");
        return "other";
    }
    else if (demographicsSplit.Length == 1)
    {
        //Console.WriteLine($"1: {string.Join(" ", demographicsSplit)}");
        if (demographicsSplit[0] == "Kids")
        {
            return "other";
        }
        else return demographicsSplit[0];

    }
    else if (demographicsSplit.Length == 2)
    {
        //Console.WriteLine($"2: {string.Join(" ", demographicsSplit)}");
        if (demographicsSplit[0] == "Kids")
        {
            return demographicsSplit[1];
        }

        return demographicsSplit[0];
    }

    return "";
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


List<Author> ExtractAuthors(string authors, string title)
{
    List<Author> result = new List<Author>();
    try
    {
        authors = authors.Replace("'", "\"");
        IList<AuthorCSV>? authorsDeserialized = JsonSerializer.Deserialize<IList<AuthorCSV>>(authors);
        foreach (AuthorCSV author in authorsDeserialized)
        {
            author.Role = author.Role == "Story & Art" ? "StoryAndArt" : author.Role;

            Author mappedAuthor = new Author
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = author.FirstName,
                LastName = author.LastName,
                Role = author.Role,
            };
            result.Add(mappedAuthor);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"{title}, {authors}");
    }

    return result;
}