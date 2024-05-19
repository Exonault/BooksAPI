using AutoMapper;
using BooksAPI.BE.Contracts.LibraryManga;
using BooksAPI.BE.Contracts.UserManga;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace BooksAPI.BE.Services;

public class UserMangaService : IUserMangaService
{
    private readonly IUserMangaRepository _userMangaRepository;
    private readonly IValidator<UserManga> _validator;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ILibraryMangaRepository _libraryMangaRepository;


    public UserMangaService(IUserMangaRepository userMangaRepository, IValidator<UserManga> validator, IMapper mapper,
        ILibraryMangaRepository libraryMangaRepository, IUserRepository userRepository)
    {
        _userMangaRepository = userMangaRepository;
        _validator = validator;
        _mapper = mapper;
        _libraryMangaRepository = libraryMangaRepository;
        _userRepository = userRepository;
    }

    public async Task CreateUserManga(CreateUserMangaRequest request)
    {
        User? user = await _userRepository.GetById(request.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.ValidationMessages.UserNotFound);
        }

        LibraryManga? libraryManga = await _libraryMangaRepository.GetLibraryMangaById(request.LibraryMangaId);

        if (libraryManga is null)
        {
            throw new ResourceNotFoundException(LibraryMangaMessages.NoLibraryComicWithId);
        }

        UserManga? possibleEntry = await _userMangaRepository
            .GetUserMangaByUserIdAndLibraryMangaId(user.Id, libraryManga.Id);

        if (possibleEntry is null)
        {
            throw new UserMangaAlreadyExistsException(UserMangaMessages.UserMangaAlreadyCreated);
        }

        UserManga userManga = _mapper.Map<UserManga>(request);

        userManga.LibraryManga = libraryManga;
        userManga.User = user;

        ValidationResult validationResult = await _validator.ValidateAsync(userManga);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _userMangaRepository.CreateUserManga(userManga);
    }

    public async Task<UserMangaResponse> GetUserManga(int id)
    {
        UserManga? userManga = await _userMangaRepository.GetUserMangaById(id);

        if (userManga is null)
        {
            throw new ResourceNotFoundException(UserMangaMessages.NoUserMangaWithId);
        }

        UserMangaResponse response = _mapper.Map<UserMangaResponse>(userManga);

        LibraryMangaResponse libraryMangaResponse = _mapper.Map<LibraryMangaResponse>(userManga.LibraryManga);

        response.LibraryMangaResponse = libraryMangaResponse;
        response.UserId = userManga.User.Id;

        return response;
    }

    public async Task<List<UserMangaResponse>> GetAllUserMangas()
    {
        List<UserManga> allUserMangas = await _userMangaRepository.GetAllUserManga();
        List<UserMangaResponse> response = ConvertToUserMangaResponseList(allUserMangas);

        return response;
    }

    public async Task<List<UserMangaResponse>> GetAllUserMangasByUserId(string id)
    {
        User? user = await _userRepository.GetById(id);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.ValidationMessages.UserNotFound);
        }

        List<UserManga> userMangas = await _userMangaRepository.GetUserMangaByUserId(id);

        List<UserMangaResponse> response = ConvertToUserMangaResponseList(userMangas);

        return response;
    }

    public async Task UpdateUserManga(int id, UpdateUserMangaRequest request)
    {
        User? user = await _userRepository.GetById(request.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.ValidationMessages.UserNotFound);
        }

        LibraryManga? libraryManga = await _libraryMangaRepository.GetLibraryMangaById(request.LibraryMangaId);

        if (libraryManga is null)
        {
            throw new ResourceNotFoundException(LibraryMangaMessages.NoLibraryComicWithId);
        }

        UserManga userManga = _mapper.Map<UserManga>(request);

        userManga.LibraryManga = libraryManga;
        userManga.User = user;

        ValidationResult validationResult = await _validator.ValidateAsync(userManga);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        UserManga? userMangaById = await _userMangaRepository.GetUserMangaById(id);

        if (userMangaById is null)
        {
            throw new ResourceNotFoundException(UserMangaMessages.NoUserMangaWithId);
        }

        UserManga updatedManga = _mapper.Map(request, userMangaById);
        await _userMangaRepository.UpdateUserManga(updatedManga);
    }

    public async Task DeleteUserManga(int id, string userId)
    {
        User? user = await _userRepository.GetById(userId);

        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.ValidationMessages.UserNotFound);
        }

        UserManga? userManga = await _userMangaRepository.GetUserMangaById(id);

        if (userManga is null)
        {
            throw new ResourceNotFoundException(UserMangaMessages.NoUserMangaWithId);
        }

        if (userManga.User.Id != userId)
        {
            throw new InvalidOperationException(UserMangaMessages.DeleteImpossible);
        }

        await _userMangaRepository.DeleteUserManga(userManga);
    }

    private List<UserMangaResponse> ConvertToUserMangaResponseList(List<UserManga> userMangas)
    {
        List<UserMangaResponse> response = new List<UserMangaResponse>();

        foreach (UserManga userManga in userMangas)
        {
            UserMangaResponse userMangaResponse = _mapper.Map<UserMangaResponse>(userManga);
            LibraryMangaResponse libraryMangaResponse = _mapper.Map<LibraryMangaResponse>(userManga.LibraryManga);

            userMangaResponse.LibraryMangaResponse = libraryMangaResponse;

            response.Add(userMangaResponse);
        }

        return response;
    }
}