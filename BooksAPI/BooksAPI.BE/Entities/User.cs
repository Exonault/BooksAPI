﻿using Microsoft.AspNetCore.Identity;

namespace BooksAPI.BE.Entities;

public class User:IdentityUser
{
    public List<UserComic> UserComics { get; set; }

    public List<Order> Orders { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiry { get; set; }
}