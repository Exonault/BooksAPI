using System.Text;
using AutoMapper;
using Blazored.SessionStorage;
using BooksAPI.FE.Authentication;
using BooksAPI.FE.Components;
using BooksAPI.FE.Constants;
using BooksAPI.FE.Interfaces;
using BooksAPI.FE.Mapping;
using BooksAPI.FE.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddMudServices();
builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddScoped<ILibraryMangaService, LibraryMangaService>();
builder.Services.AddScoped<IUserMangaService, UserMangaService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredSessionStorage();

MapperConfiguration mapperConfig = new MapperConfiguration(config => { config.AddProfile(new UserProfile()); });

builder.Services.AddSingleton(mapperConfig.CreateMapper());

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters()
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateIssuerSigningKey = true,
//             ValidateLifetime = true,
//             ValidIssuer = configuration["Jwt:Issuer"],
//             ValidAudience = configuration["Jwt:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
//         };
//     });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(ApplicationConstants.PolicyNames.AdminRolePolicyName,
        p =>
        {
            p.RequireClaim(ApplicationConstants.ClaimTypes.ClaimRoleType,
                ApplicationConstants.ClaimNames.AdminRoleClaimName);
        });

    options.AddPolicy(ApplicationConstants.PolicyNames.UserRolePolicyName,
        p =>
        {
            p.RequireClaim(ApplicationConstants.ClaimTypes.ClaimRoleType,
                ApplicationConstants.ClaimNames.UserRoleClaimName);
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AllowAnonymous();

app.Run();