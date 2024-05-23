using AutoMapper;
using BooksAPI.BE.Contracts.Author;
using BooksAPI.BE.Contracts.LibraryManga;
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

        List<Author> authorsFiltered = new List<Author>();
        foreach (Author author in libraryManga.Authors)
        {
            Author? searchedAuthor = await _authorRepository.GetAuthor(author.FirstName, author.LastName, author.Role);

            if (searchedAuthor is null)
            {
                authorsFiltered.Add(author);
            }
            else
            {
                authorsFiltered.Remove(author);
                authorsFiltered.Add(searchedAuthor);
            }
        }

        libraryManga.Authors = authorsFiltered;

        await _libraryMangaRepository.CreateLibraryManga(libraryManga);
    }

    public async Task<LibraryMangaResponse> GetLibraryManga(int id)
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

    public async Task<List<LibraryMangaResponse>> SearchByTitle(string title)
    {
        List<LibraryManga> searchResult = await _libraryMangaRepository.SearchByTitle(title);

        return _mapper.Map<List<LibraryMangaResponse>>(searchResult);
    }

    public async Task<List<LibraryMangaForPageResponse>> GetLibraryMangasForPage(int pageIndex, int pageEntriesCount)
    {
        List<LibraryManga> libraryMangasForPage = await _libraryMangaRepository
            .GetLibraryMangasForPage(pageIndex - 1, pageEntriesCount); //page 1 = pageIndex 0

        return _mapper.Map<List<LibraryMangaForPageResponse>>(libraryMangasForPage);
    }

    public async Task UpdateLibraryManga(int id, UpdateLibraryMangaRequest request)
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


        List<Author> authorsFiltered = new List<Author>();

        foreach (Author author in updatedManga.Authors)
        {
            Author? searchedAuthor = await _authorRepository.GetAuthor(author.FirstName, author.LastName, author.Role);

            if (searchedAuthor is null)
            {
                authorsFiltered.Add(author);
            }
            else
            {
                authorsFiltered.Remove(author);
                authorsFiltered.Add(searchedAuthor);
            }
        }

        updatedManga.Authors = authorsFiltered;

        await _libraryMangaRepository.UpdateLibraryManga(updatedManga);
    }

    public async Task DeleteLibraryManga(int id)
    {
        LibraryManga? libraryManga = await _libraryMangaRepository.GetLibraryMangaById(id);

        if (libraryManga is null)
        {
            throw new ResourceNotFoundException(LibraryMangaMessages.NoLibraryComicWithId);
        }

        await _libraryMangaRepository.DeleteLibraryManga(libraryManga);
    }
}