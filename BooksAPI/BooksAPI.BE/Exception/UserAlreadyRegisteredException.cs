﻿namespace BooksAPI.BE.Exception;

public class UserAlreadyRegisteredException:System.Exception
{
    public UserAlreadyRegisteredException()
    {
    }

    public UserAlreadyRegisteredException(string? message) : base(message)
    {
    }

    public UserAlreadyRegisteredException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}