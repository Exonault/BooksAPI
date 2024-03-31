using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BooksAPI.BE.Constants;
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

    private static readonly TimeSpan TokenDuration = TimeSpan.FromHours(1);
    
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
        }

        IList<string> roles = await _userManager.GetRolesAsync(getUser);

        UserSession userSession = new UserSession(getUser.Id, getUser.Email!, roles);
        
        string token = GenerateToken(userSession);

        return token;
    }

    private string GenerateToken(UserSession user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(AppConstants.ClaimTypes.ClaimUserIdType, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        ];
        
        foreach (string role in user.Roles)
        {
            claims.Add(new Claim(AppConstants.ClaimTypes.ClaimRoleType, role));
        }

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenDuration),
            Issuer = _config["Jwt:Issuer"],
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Audience = _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        string jwt = tokenHandler.WriteToken(token);

        return jwt;
    }
    
    private record UserSession(string Id, string Email, IEnumerable<string> Roles);
}