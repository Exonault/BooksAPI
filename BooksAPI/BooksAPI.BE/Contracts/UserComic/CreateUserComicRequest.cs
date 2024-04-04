﻿namespace BooksAPI.BE.Contracts.UserComic;

public class CreateUserComicRequest
{
    public string ReadingStatus { get; set; } = string.Empty;
    
    public int ReadVolumes { get; set; }

    public int CollectedVolumes { get; set; }
    
    public decimal Price { get; set; }

    public string CollectionStatus { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public Guid LibraryComicId { get; set; }
}