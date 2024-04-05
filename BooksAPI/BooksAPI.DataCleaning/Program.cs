using System.Globalization;
using System.Text.Json;
using BooksAPI.DataCleaning;
using CsvHelper;
using CsvHelper.Configuration;

Console.WriteLine("Hello, World!");

string filePath = @"C:\Users\k.krachmarov\Desktop\BooksAPI\BooksAPI\BooksAPI.DataCleaning\manga - Copy.csv";
if (File.Exists(filePath))
{
    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
    };

    // Open the file for reading
    using (var reader = new StreamReader(filePath))
    using (var csv = new CsvReader(reader, csvConfig))
    {
        // Read the CSV records
        csv.Read();
        csv.ReadHeader();

        // Iterate over each record
        while (csv.Read())
        {
            //string mangaId = csv.GetField("manga_id");
            string title = csv.GetField("title");
            string type = csv.GetField("type");
            // string score = csv.GetField("score");
            //string scoredBy = csv.GetField("scored_by");
            string status = csv.GetField("status");
            string volumes = csv.GetField("volumes");
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
            string demographics = csv.GetField("demographics");
            string authors = csv.GetField("authors");
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
            authors = authors.Replace("'", "\"");
            IList<Author>? deserialize = JsonSerializer.Deserialize<IList<Author>>(authors);
            Console.WriteLine($"{title}; {type}; {status}; {volumes}; {demographics}; {authors}");
        }
    }
}
else
{
    Console.WriteLine("File not found: " + filePath);
}
// if (File.Exists(filePath))
// {
//     // Open the file for reading
//     using (StreamReader reader = new StreamReader(filePath))
//     {
//         while (!reader.EndOfStream)
//         {
//             string line = reader.ReadLine();
//             
//             string[] fields = line.Split(',');
//             
//             foreach (string field in fields)
//             {
//                 Console.Write($"{field} ");
//             }
//         
//             Console.WriteLine();
//         }
//
//         
//     }
// }
// else
// {
//     Console.WriteLine("File not found: " + filePath);
// }