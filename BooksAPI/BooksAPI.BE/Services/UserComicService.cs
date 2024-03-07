using AutoMapper;
using BooksAPI.BE.Contracts.LibraryComic;
using BooksAPI.BE.Contracts.UserComic;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Messages;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace BooksAPI.BE.Services;

public class UserComicService : IUserComicService
{
    private readonly IUserComicRepository _userComicRepository;
    private readonly IValidator<UserComic> _validator;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ILibraryComicRepository _libraryComicRepository;


    public UserComicService(IUserComicRepository userComicRepository, IValidator<UserComic> validator, IMapper mapper,
        UserManager<User> userManager, ILibraryComicRepository libraryComicRepository)
    {
        _userComicRepository = userComicRepository;
        _validator = validator;
        _mapper = mapper;
        _userManager = userManager;
        _libraryComicRepository = libraryComicRepository;
    }

    public async Task CreateUserComic(CreateUserComicRequest request)
    {
        User? user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.UserNotFound);
        }

        LibraryComic? libraryComic = await _libraryComicRepository.GetLibraryComicById(request.LibraryComicId);

        if (libraryComic is null)
        {
            throw new ResourceNotFoundException(LibraryComicMessages.NoLibraryComicWithId);
        }

        UserComic userComic = _mapper.Map<UserComic>(request);

        userComic.LibraryComic = libraryComic;
        userComic.User = user;

        ValidationResult validationResult = await _validator.ValidateAsync(userComic);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _userComicRepository.CreateUserComic(userComic);
    }

    public async Task<UserComicResponse> GetUserComic(Guid id)
    {
        UserComic? userComic = await _userComicRepository.GetUserComicById(id);

        if (userComic is null)
        {
            throw new ResourceNotFoundException(UserComicMessages.NoUserComicWithId);
        }

        UserComicResponse response = _mapper.Map<UserComicResponse>(userComic);

        LibraryComicResponse libraryComicResponse = _mapper.Map<LibraryComicResponse>(userComic.LibraryComic);

        response.LibraryComicResponse = libraryComicResponse;
        response.UserId = userComic.User.Id;

        return response;
    }

    public async Task<List<UserComicResponse>> GetAllUserComics()
    {
        List<UserComic> allUserComics = await _userComicRepository.GetAllUserComic();
        List<UserComicResponse> response = ConvertToUserComicResponseList(allUserComics);

        return response;
    }

    public async Task<List<UserComicResponse>> GetAllUserComicsByUserId(string id)
    {
        User? user = await _userManager.FindByIdAsync(id);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.UserNotFound);
        }

        List<UserComic> userComics = await _userComicRepository.GetUserComicsByUserId(id);

        List<UserComicResponse> response = ConvertToUserComicResponseList(userComics);

        return response;
    }

    public async Task UpdateUserComic(Guid id, UpdateUserComicRequest request)
    {
        User? user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.UserNotFound);
        }

        LibraryComic? libraryComic = await _libraryComicRepository.GetLibraryComicById(request.LibraryComicId);

        if (libraryComic is null)
        {
            throw new ResourceNotFoundException(LibraryComicMessages.NoLibraryComicWithId);
        }

        UserComic userComic = _mapper.Map<UserComic>(request);

        userComic.LibraryComic = libraryComic;
        userComic.User = user;

        ValidationResult validationResult = await _validator.ValidateAsync(userComic);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        UserComic? userComicById = await _userComicRepository.GetUserComicById(id);

        _mapper.Map(userComic, userComicById);
        await _userComicRepository.UpdateUserComic(userComic);
    }

    public async Task DeleteUserComic(Guid id, string userId)
    {
        User? user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.UserNotFound);
        }

        UserComic? userComic = await _userComicRepository.GetUserComicById(id);

        if (userComic is null)
        {
            throw new ResourceNotFoundException(UserComicMessages.NoUserComicWithId);
        }

        if (userComic.User.Id != userId)
        {
            throw new InvalidOperationException(UserComicMessages.DeleteImpossible);
        }

        await _userComicRepository.DeleteUserComic(userComic);
    }

    private List<UserComicResponse> ConvertToUserComicResponseList(List<UserComic> userComics)
    {
        List<UserComicResponse> response = new List<UserComicResponse>();

        foreach (UserComic userComic in userComics)
        {
            UserComicResponse userComicResponse = _mapper.Map<UserComicResponse>(userComic);
            LibraryComicResponse libraryComicResponse = _mapper.Map<LibraryComicResponse>(userComic.LibraryComic);

            userComicResponse.LibraryComicResponse = libraryComicResponse;

            response.Add(userComicResponse);
        }

        return response;
    }
}