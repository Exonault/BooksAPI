﻿using BooksAPI.BE.Contracts.User;

namespace BooksAPI.BE.Interfaces.Services;
public interface IUserService
{
    Task<RegisterResponse> RegisterAccount(RegisterRequest request);

    Task<LoginResponse> LoginAccount(LoginRequest request);

    Task Refresh(RefreshRequest request);

    Task Revoke();
}