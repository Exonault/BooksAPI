﻿namespace BooksAPI.BE.Contracts.UserManga;

public class CreateUserMangaRequest
{
    public string ReadingStatus { get; set; } = string.Empty;
    
    public int ReadVolumes { get; set; }

    public int CollectedVolumes { get; set; }
    
    public decimal PricePerVolume { get; set; }

    public string CollectionStatus { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public int LibraryMangaId { get; set; }
}