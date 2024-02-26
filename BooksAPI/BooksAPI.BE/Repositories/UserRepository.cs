using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Exception;
using BooksAPI.BE.Interfaces.Repositories;
using BooksAPI.BE.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BooksAPI.BE.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    public async Task Register(User newUser, bool admin)
    {
        User? user = await _userManager.FindByEmailAsync(newUser.Email);

        if (user is not null)
        {
            throw new UserAlreadyRegisteredException(UserMessages.AlreadyRegistered);
        }

        IdentityResult createdUser = await _userManager.CreateAsync(newUser, newUser.PasswordHash);

        if (!createdUser.Succeeded)
        {
            throw new System.Exception(UserMessages.ErrorOccured);
        }


        await CreateRoles();

        if (admin)
        {
            await _userManager.AddToRoleAsync(newUser, "Admin");
        }

        await _userManager.AddToRoleAsync(newUser, "User");


        // IdentityRole? checkAdmin = await _roleManager.FindByNameAsync("Admin");
        //
        // if (checkAdmin is null)
        // {
        //     await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
        //     await _userManager.AddToRoleAsync(newUser, "Admin");
        // }
        // else
        // {
        //     IdentityRole? checkedUser = await _roleManager.FindByNameAsync("User");
        //     if (checkedUser is null)
        //     {
        //         await _roleManager.CreateAsync(new IdentityRole()
        //         {
        //             Name = "User"
        //         });
        //     }
        //     await _userManager.AddToRoleAsync(newUser, "User");
        // }
    }

    private async Task CreateRoles()
    {
        IdentityRole? checkAdmin = await _roleManager.FindByNameAsync("Admin");
        if (checkAdmin is null)
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
        }

        IdentityRole? checkedUser = await _roleManager.FindByNameAsync("User");
        if (checkedUser is null)
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
        }
    }

    public async Task<string> Login(String email, String password)
    {
        User? getUser = await _userManager.FindByEmailAsync(email);

        if (getUser is null)
        {
            throw new UserNotFoundException(UserMessages.UserNotFound);
        }

        bool checkUserPassword = await _userManager.CheckPasswordAsync(getUser, password);

        if (!checkUserPassword)
        {
            throw new InvalidEmailPasswordException(UserMessages.InvalidEmailPassword);
            //return new LoginResponse(false, null!, UserMessages.InvalidEmailPassword);
        }

        IList<string> roles = await _userManager.GetRolesAsync(getUser);

        var userSession = new UserSession(getUser.Id, getUser.Email, roles.First());

        string token = GenerateToken(userSession);

        return token;
    }

    private string GenerateToken(UserSession user)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        Claim[] userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, user.Role!),
        };

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public record UserSession(string? Id, string? Email, string? Role);
}