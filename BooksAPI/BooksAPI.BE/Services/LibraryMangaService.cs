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

public class LibraryMangaService : ILibraryMangaService
{
    private readonly ILibraryMangaRepository _libraryMangaRepository;
    private readonly IValidator<LibraryManga> _validator;
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;


    public LibraryMangaService(ILibraryMangaRepository libraryMangaRepository, IValidator<LibraryManga> validator,
        IMapper mapper, IAuthorRepository authorRepository)
    {
        _libraryMangaRepository = libraryMangaRepository;
        _validator = validator;
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task CreateLibraryManga(CreateLibraryMangaRequest request)
    {
        LibraryManga libraryManga = _mapper.Map<LibraryManga>(request);

        ValidationResult validationResult = await _validator.ValidateAsync(libraryManga);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        foreach (AuthorRequest authorRequest in request.Authors)
        {
            Author? searchedAuthor =
                await _authorRepository.GetAuthor(authorRequest.FirstName, authorRequest.LastName, authorRequest.Role);

            if (searchedAuthor is null)
            {
                Author author = _mapper.Map<Author>(authorRequest);
                libraryManga.Authors.Add(author);
            }
            else
            {
                libraryManga.Authors.Add(searchedAuthor);
            }
        }

        await _libraryMangaRepository.CreateLibraryManga(libraryManga);
    }

    public async Task<LibraryMangaResponse> GetLibraryManga(Guid id)
    {
        LibraryManga? libraryManga = await _libraryMangaRepository.GetLibraryMangaById(id);
        if (libraryManga is null)
        {
            throw new ResourceNotFoundException(LibraryMangaMessages.NoLibraryComicWithId);
        }

        return _mapper.Map<LibraryMangaResponse>(libraryManga);
    }

    public async Task<List<LibraryMangaResponse>> GetAllLibraryMangas()
    {
        List<LibraryManga> allLibraryMangas = await _libraryMangaRepository.GetAllLibraryMangas();

        return _mapper.Map<List<LibraryMangaResponse>>(allLibraryMangas);
    }

    public async Task<List<LibraryMangaResponse>> GetLibraryMangasForPage(int pageIndex, int pageEntriesCount)
    {
        List<LibraryManga> libraryMangasForPage = await _libraryMangaRepository
            .GetLibraryMangasForPage(pageIndex - 1, pageEntriesCount);//page 1 = pageIndex 0

        return _mapper.Map<List<LibraryMangaResponse>>(libraryMangasForPage);
    }

    public async Task UpdateLibraryManga(Guid id, UpdateLibraryMangaRequest request)
    {
        LibraryManga? libraryManga = await _libraryMangaRepository.GetLibraryMangaById(id);

        if (libraryManga is null)
        {
            throw new ResourceNotFoundException(LibraryMangaMessages.NoLibraryComicWithId);
        }

        LibraryManga newManga = _mapper.Map<LibraryManga>(request);

        ValidationResult validationResult = await _validator.ValidateAsync(newManga);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        LibraryManga updatedManga = _mapper.Map(request, libraryManga);

        foreach (AuthorRequest authorRequest in request.Authors)
        {
            Author? searchAuthor =
                await _authorRepository.GetAuthor(authorRequest.FirstName, authorRequest.LastName, authorRequest.Role);

            if (searchAuthor is null)
            {
                Author author = _mapper.Map<Author>(authorRequest);
                updatedManga.Authors.Add(author);
                await _authorRepository.CreateAuthor(author);
            }
            else
            {
                updatedManga.Authors.Add(searchAuthor);
            }
        }

        await _libraryMangaRepository.UpdateLibraryManga(updatedManga);
    }

    public async Task DeleteLibraryManga(Guid id)
    {
        LibraryManga? libraryManga = await _libraryMangaRepository.GetLibraryMangaById(id);

        if (libraryManga is null)
        {
            throw new ResourceNotFoundException(LibraryMangaMessages.NoLibraryComicWithId);
        }

        await _libraryMangaRepository.DeleteLibraryManga(libraryManga);
    }
}