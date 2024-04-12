using BooksAPI.FE.Data;

namespace BooksAPI.FE.Interfaces;

public interface IUserService
{
    public Task<HttpResponseMessage> Register(RegisterModel model);

    // public Task Login();
    //
    // public Task Refresh();
}