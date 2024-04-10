using BooksAPI.BE.Entities;
using BooksAPI.BE.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BooksAPI.BE.Repositories;

public class UserRepository:IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetByName(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<User?> GetById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<bool> CheckPassword(User user, string password)
    {
       return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> Create(User user, string password)
    {
        IdentityResult identityResult = await _userManager.CreateAsync(user, password);

        return identityResult;
    }

    public async Task AddToRole(User user, string role)
    {
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<List<string>> GetAllRoles(User user)
    {
        IList<string> rolesAsync = await _userManager.GetRolesAsync(user);

        return rolesAsync.ToList();

    }

    public async Task UpdateUser(User user)
    {
        await _userManager.UpdateAsync(user);
    }

    // public Task Register(User newUser, bool admin)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<string> Login(string userName, string password)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<string> Refresh(string token, string refreshToken)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task Revoke()
    // {
    //     throw new NotImplementedException();
    // }

}