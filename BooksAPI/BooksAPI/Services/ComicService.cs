using AutoMapper;
using BooksAPI.Contracts.Requests.Comic;
using BooksAPI.Contracts.Response.Comic;
using BooksAPI.Entities;
using BooksAPI.Exception;
using BooksAPI.Interfaces.Repositories;
using BooksAPI.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;

namespace BooksAPI.Services;

public class ComicService : IComicService
{
    private readonly IComicRepository _comicRepository;
    private readonly IValidator<Comic> _validator;
    private readonly IMapper _mapper;


    public ComicService(IComicRepository comicRepository, IValidator<Comic> validator, IMapper mapper)
    {
        _comicRepository = comicRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CreateComicResponse> CreateComic(CreateComicRequest request)
    {
        Comic comic = _mapper.Map<Comic>(request);

        ValidationResult validationResult = _validator.Validate(comic);


        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        try
        {
            comic = await _comicRepository.CreateComic(comic);
        }
        catch (System.Exception)
        {
            throw;
        }

        return _mapper.Map<CreateComicResponse>(comic);
    }

    public async Task<GetComicResponse> GetComic(Guid id)
    {
        Comic? comic = await _comicRepository.GetComicById(id);

        if (comic is null)
        {
            throw new ResourceNotFoundException("Comic with id doesn't exist.");
        }

        return _mapper.Map<GetComicResponse>(comic);
    }

    public async Task<List<GetComicResponse>> GetAllComics()
    {
        List<Comic> comics = await _comicRepository.GetAllComics();

        return _mapper.Map<List<GetComicResponse>>(comics);
    }

    public async Task<UpdateComicResponse> UpdateComic(Guid id, UpdateComicRequest request)
    {
        Comic? comic = await _comicRepository.GetComicById(id);

        if (comic is null)
        {
            throw new ResourceNotFoundException("Comic with this id doesn't exists");
        }

        Comic updatedComic = _mapper.Map<Comic>(request);

        ValidationResult validationResult = _validator.Validate(updatedComic);


        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        try
        {
            _mapper.Map(request, comic);
            comic = await _comicRepository.UpdateComic(comic);
        }
        catch (System.Exception)
        {
            throw;
        }

        return _mapper.Map<UpdateComicResponse>(comic);
    }

    public async Task<DeleteComicResponse> DeleteComic(Guid id)
    {
        Comic? comic = await _comicRepository.GetComicById(id);

        if (comic is null)
        {
            throw new ResourceNotFoundException("Comic with this id doesn't exists");
        }

        try
        {
            await _comicRepository.DeleteComic(comic);
        }
        catch (System.Exception e)
        {
            throw;
        }

        return _mapper.Map<DeleteComicResponse>(comic);
    }
}