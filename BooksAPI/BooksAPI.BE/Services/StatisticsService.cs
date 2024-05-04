using BooksAPI.BE.Constants;
using BooksAPI.BE.Contracts.Statistics.Order;
using BooksAPI.BE.Contracts.Statistics.UserManga;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Interfaces.Services;
using BooksAPI.BE.Messages;

namespace BooksAPI.BE.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserMangaRepository _userMangaRepository;
    private readonly IUserRepository _userRepository;

    public StatisticsService(IOrderRepository orderRepository, IUserMangaRepository userMangaRepository,
        IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _userMangaRepository = userMangaRepository;
        _userRepository = userRepository;
    }

    public async Task<List<UserMangaDemographicResponse>> GetUserMangaBreakdownByDemographic(string userId)
    {
        await ValidateUser(userId);

        List<UserManga> userMangas = await _userMangaRepository.GetUserMangaByUserId(userId);

        List<UserMangaDemographicResponse> response = userMangas.GroupBy(x => x.LibraryManga.DemographicType)
            .Select(x => new UserMangaDemographicResponse()
            {
                DemographicType = x.Key,
                Count = x.Count(),
            }).ToList();

        return response;
    }


    public async Task<List<UserMangaTypeResponse>> GetUserMangaBreakdownByType(string userId)
    {
        await ValidateUser(userId);

        List<UserManga> userMangas = await _userMangaRepository.GetUserMangaByUserId(userId);

        List<UserMangaTypeResponse> response = userMangas.GroupBy(x => x.LibraryManga.Type)
            .Select(x => new UserMangaTypeResponse()
            {
                Type = x.Key,
                Count = x.Count(),
            }).ToList();

        return response;
    }

    public async Task<List<UserMangaPublishingStatusResponse>> GetUserMangaBreakdownByPublishingStatus(string userId)
    {
        await ValidateUser(userId);

        List<UserManga> userMangas = await _userMangaRepository.GetUserMangaByUserId(userId);

        List<UserMangaPublishingStatusResponse> response = userMangas.GroupBy(x => x.LibraryManga.PublishingStatus)
            .Select(x => new UserMangaPublishingStatusResponse()
            {
                PublishingStatus = x.Key,
                Count = x.Count(),
            })
            .ToList();

        return response;
    }

    public async Task<List<UserMangaReadingStatusResponse>> GetUserMangaBreakdownByReadingStatus(string userId)
    {
        await ValidateUser(userId);

        List<UserManga> userMangas = await _userMangaRepository.GetUserMangaByUserId(userId);

        List<UserMangaReadingStatusResponse> response = userMangas.GroupBy(x => x.ReadingStatus)
            .Select(x => new UserMangaReadingStatusResponse()
            {
                ReadingStatus = x.Key,
                Count = x.Count(),
            })
            .ToList();

        return response;
    }

    public async Task<List<UserMangaCollectionStatusResponse>> GetUserMangaBreakdownByCollectionStatus(string userId)
    {
        await ValidateUser(userId);

        List<UserManga> userMangas = await _userMangaRepository.GetUserMangaByUserId(userId);

        List<UserMangaCollectionStatusResponse> response = userMangas.GroupBy(x => x.CollectionStatus)
            .Select(x => new UserMangaCollectionStatusResponse()
            {
                CollectionStatus = x.Key,
                Count = x.Count(),
            })
            .ToList();

        return response;
    }

    public async Task<UserMangaTotalSpendingResponse> GetUserMangaBreakdownFromTotalSpending(string userId)
    {
        await ValidateUser(userId);

        List<UserManga> userMangas = await _userMangaRepository.GetUserMangaByUserId(userId);

        decimal totalPrice = 0.0m;

        List<MangaResponse> mappings = new List<MangaResponse>();

        foreach (UserManga userManga in userMangas)
        {
            if (UserMangaConstants.CollectingStatus.PlanToCollect == userManga.CollectionStatus) // skip plan to collect
            {
                continue;
            }

            decimal priceForSeries = userManga.PricePerVolume * userManga.CollectedVolumes;
            MangaResponse mapping = new MangaResponse()
            {
                Price = priceForSeries,
                Title = userManga.LibraryManga.Title,
            };

            mappings.Add(mapping);
            totalPrice += priceForSeries;
        }

        UserMangaTotalSpendingResponse response = new UserMangaTotalSpendingResponse()
        {
            TotalSpending = totalPrice,
            Mangas = mappings
        };

        return response;
    }

    public async Task<GeneralStatisticsResponse> GetGeneralStatistics(string userId)
    {
        await ValidateUser(userId);

        List<UserManga> userMangas = await _userMangaRepository.GetUserMangaByUserId(userId);

        decimal totalSpending = Decimal.Zero;
        int totalCollectedVolumes = 0;
        int totalReadVolumes = 0;

        foreach (UserManga userManga in userMangas)
        {
            if (UserMangaConstants.CollectingStatus.PlanToCollect == userManga.CollectionStatus)
            {
                continue;
            }

            totalCollectedVolumes += userManga.CollectedVolumes;
            totalReadVolumes += userManga.ReadVolumes;
            totalSpending += userManga.CollectedVolumes * userManga.PricePerVolume;
        }

        GeneralStatisticsResponse response = new GeneralStatisticsResponse
        {
            TotalSpending = totalSpending,
            TotalCollectedVolumes = totalCollectedVolumes,
            TotalReadVolumes = totalReadVolumes
        };

        return response;
    }

    public async Task<List<OrdersByYearResponse>> GetOrderBreakdownByYear(string userId)
    {
        await ValidateUser(userId);
        List<Order> orders = await _orderRepository.GetAllOrdersByUserId(userId);

        List<OrdersByYearResponse> response = orders.GroupBy(x => x.Date.Year)
            .Select(x => new OrdersByYearResponse()
            {
                Year = x.Key,
                Items = x.Sum(o => o.NumberOfItems),
                Price = x.Sum(o => o.Amount)
            })
            .ToList();

        return response;
    }

    public async Task<List<OrdersForMonthByYearResponse>> GetOrderBreakdownForMonthsByYear(string userId, int year)
    {
        await ValidateUser(userId);

        List<Order> orders = await _orderRepository.GetAllOrdersByUserId(userId);

        List<Order> ordersFromYear = orders.Where(x => x.Date.Year == year).ToList();

        List<OrdersForMonthByYearResponse> response = ordersFromYear.GroupBy(x => x.Date.Month)
            .Select(x => new OrdersForMonthByYearResponse()
            {
                Month = x.Key,
                Items = x.Sum(o => o.NumberOfItems),
                Price = x.Sum(o => o.Amount)
            })
            .ToList();

        return response;
    }

    public async Task<List<OrderByPlaceResponse>> GetOrderBreakdownByPlace(string userId)
    {
        await ValidateUser(userId);

        List<Order> orders = await _orderRepository.GetAllOrdersByUserId(userId);

        List<OrderByPlaceResponse> result = orders.GroupBy(o => o.Place).Select(x => new OrderByPlaceResponse
            {
                Place = x.Key,
                TotalOrders = x.Count(),
                TotalValueOfOrders = x.Sum(o => o.Amount)
            })
            .ToList();

        return result;
    }

    private async Task ValidateUser(string userId)
    {
        User? user = await _userRepository.GetById(userId);
        if (user is null)
        {
            throw new UserNotFoundException(UserMessages.ValidationMessages.UserNotFound);
        }
    }
}