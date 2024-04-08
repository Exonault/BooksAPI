using AutoMapper;
using BooksAPI.BE.Contracts.Author;
using BooksAPI.BE.Contracts.LibraryComic;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace BooksAPI.BE.Services;

public class LibraryComicService : ILibraryComicService
{
    private readonly ILibraryComicRepository _libraryComicRepository;
    private readonly IValidator<LibraryComic> _validator;
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;


    public LibraryComicService(ILibraryComicRepository libraryComicRepository, IValidator<LibraryComic> validator,
        IMapper mapper, IAuthorRepository authorRepository)
    {
        _libraryComicRepository = libraryComicRepository;
        _validator = validator;
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task CreateLibraryComic(CreateLibraryComicRequest request)// TODO fix for new authors
    {
        LibraryComic libraryComic = _mapper.Map<LibraryComic>(request);

        ValidationResult validationResult = await _validator.ValidateAsync(libraryComic);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        foreach (AuthorRequest authorRequest in request.Authors)
        {
            Author? searchedAuthor = await _authorRepository.GetAuthor(authorRequest.FirstName, authorRequest.LastName, authorRequest.Role);

            if (searchedAuthor is null)
            {
                Author author = _mapper.Map<Author>(authorRequest);
                libraryComic.Authors.Add(author);
            }
            else
            {
               libraryComic.Authors.Add(searchedAuthor); 
            }
            
        }

        await _libraryComicRepository.CreateLibraryComic(libraryComic);
    }

    public async Task<LibraryComicResponse> GetLibraryComic(Guid id)
    {
        LibraryComic? libraryComic = await _libraryComicRepository.GetLibraryComicById(id);
        if (libraryComic is null)
        {
            throw new ResourceNotFoundException(LibraryComicMessages.NoLibraryComicWithId);
        }

        return _mapper.Map<LibraryComicResponse>(libraryComic);
    }

    public async Task<List<LibraryComicResponse>> GetAllLibraryComics()
    {
        List<LibraryComic> allLibraryComics = await _libraryComicRepository.GetAllLibraryComics();

        return _mapper.Map<List<LibraryComicResponse>>(allLibraryComics);
    }

    public async Task UpdateLibraryComic(Guid id, UpdateLibraryComicRequest request) //TODO Fix for new authors
    {
        LibraryComic? libraryComic = await _libraryComicRepository.GetLibraryComicById(id);

        if (libraryComic is null)
        {
            throw new ResourceNotFoundException(LibraryComicMessages.NoLibraryComicWithId);
        }

        LibraryComic newComic = _mapper.Map<LibraryComic>(request);

        ValidationResult validationResult = await _validator.ValidateAsync(newComic);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        LibraryComic updatedComic = _mapper.Map(request, libraryComic);
        
        foreach (AuthorRequest authorRequest in request.Authors)
        {
            Author? searchAuthor = await _authorRepository.GetAuthor(authorRequest.FirstName, authorRequest.LastName, authorRequest.Role);

            if (searchAuthor is null)
            {
                Author author = _mapper.Map<Author>(authorRequest);
                updatedComic.Authors.Add(author);
                await _authorRepository.CreateAuthor(author);
            }
            else
            {
                updatedComic.Authors.Add(searchAuthor);
            }
            
        }
        
        await _libraryComicRepository.UpdateLibraryComic(updatedComic);
    }

    public async Task DeleteLibraryComic(Guid id)
    {
        LibraryComic? libraryComic = await _libraryComicRepository.GetLibraryComicById(id);

        if (libraryComic is null)
        {
            throw new ResourceNotFoundException(LibraryComicMessages.NoLibraryComicWithId);
        }

        await _libraryComicRepository.DeleteLibraryComic(libraryComic);
    }
}