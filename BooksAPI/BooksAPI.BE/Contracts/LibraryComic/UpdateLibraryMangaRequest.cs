﻿using BooksAPI.BE.Contracts.Author;

namespace BooksAPI.BE.Contracts.LibraryComic;

public class UpdateLibraryMangaRequest
{
    public string Title { get; set; } = string.Empty;
    public string DemographicType { get; set; } = string.Empty;
    public string ComicType { get; set; } = string.Empty;
    public string PublishingStatus { get; set; } = string.Empty;
    public int? TotalVolumes { get; set; }
    public List<AuthorRequest> Authors { get; set; } = new List<AuthorRequest>();
}