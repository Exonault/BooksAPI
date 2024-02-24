using AutoMapper;
using BooksAPI.BE.Contracts.LibraryComic;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;

namespace BooksAPI.BE.Services;

public class LibraryComicService : ILibraryComicService
{
    private readonly ILibraryComicRepository _libraryComicRepository;
    private readonly IValidator<LibraryComic> _validator;
    private readonly IMapper _mapper;


    public LibraryComicService(ILibraryComicRepository libraryComicRepository, IValidator<LibraryComic> validator,
        IMapper mapper)
    {
        _libraryComicRepository = libraryComicRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task CreateLibraryComic(CreateLibraryComicRequest request)
    {
        LibraryComic libraryComic = _mapper.Map<LibraryComic>(request);

        ValidationResult validationResult = await _validator.ValidateAsync(libraryComic);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        try
        {
            await _libraryComicRepository.CreateLibraryComic(libraryComic);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<LibraryComicResponse> GetLibraryComic(Guid id)
    {
        LibraryComic? libraryComic = await _libraryComicRepository.GetLibraryComicById(id);
        if (libraryComic is null)
        {
            throw new ResourceNotFoundException("Library comic with id doesn't exist.");
        }

        return _mapper.Map<LibraryComicResponse>(libraryComic);
    }

    public async Task<List<LibraryComicResponse>> GetAllLibraryComics()
    {
        List<LibraryComic> allLibraryComics = await _libraryComicRepository.GetAllLibraryComics();

        return _mapper.Map<List<LibraryComicResponse>>(allLibraryComics);
    }

    public async Task UpdateLibraryComic(Guid id, UpdateLibraryComicRequest request)
    {
        LibraryComic? libraryComic = await _libraryComicRepository.GetLibraryComicById(id);
        
        if (libraryComic is null)
        {
            throw new ResourceNotFoundException("Library comic with id doesn't exist.");
        }

        LibraryComic updatedComic = _mapper.Map<LibraryComic>(request);

        ValidationResult validationResult = await _validator.ValidateAsync(updatedComic);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        try
        {
            _mapper.Map(request, libraryComic);
            await _libraryComicRepository.UpdateLibraryComic(libraryComic);
        }
        catch (System.Exception )
        {
            throw;
        }
      
    }

    public async Task DeleteLibraryComic(Guid id)
    {
        LibraryComic? libraryComic = await _libraryComicRepository.GetLibraryComicById(id);

        if (libraryComic is null)
        {
            throw new ResourceNotFoundException("Library comic with id doesn't exist.");
        }

        try
        {
            await _libraryComicRepository.DeleteLibraryComic(libraryComic);
        }
        catch (System.Exception e)
        {
            throw;
        }
        
        throw new NotImplementedException();
    }
}