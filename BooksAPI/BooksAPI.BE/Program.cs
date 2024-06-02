using System.Text;
using AutoMapper;
using BooksAPI.BE.Constants;
using BooksAPI.BE.Data;
using BooksAPI.BE.Endpoints;
using BooksAPI.BE.Entities;
using BooksAPI.BE.Mapping;
using BooksAPI.BE.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

//Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

//DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("ApplicationDb"));
});

//Auth
builder.Services.AddIdentity<User, IdentityRole>(options => { options.User.RequireUniqueEmail = true; })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole>()
    .AddSignInManager();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
            ClockSkew = TimeSpan.FromSeconds(5)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AppConstants.PolicyNames.AdminRolePolicyName,
        p => { p.RequireClaim(AppConstants.ClaimTypes.ClaimRoleType, AppConstants.ClaimNames.AdminRoleClaimName); });

    options.AddPolicy(AppConstants.PolicyNames.UserRolePolicyName,
        p => { p.RequireClaim(AppConstants.ClaimTypes.ClaimRoleType, AppConstants.ClaimNames.UserRoleClaimName); });
});

//CORS
const string corsPolicy = "AllowedOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        policy => { policy.WithOrigins(configuration.GetSection("FrontEndUrl").Value!); });
});

//Entity Services
builder.Services.AddLibraryMangaServices();
builder.Services.AddUserMangaServices();
builder.Services.AddOrderServices();
builder.Services.AddUserServices();
builder.Services.AddStatisticsServices();

//Mapping
MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
{
    config.AddProfile(new LibraryMangaProfile());
    config.AddProfile(new UserMangaProfile());
    config.AddProfile(new OrderProfile());
    config.AddProfile(new AuthorProfile());
});

builder.Services.AddSingleton(mapperConfiguration.CreateMapper());

//Cache
builder.Services.AddOutputCache()
    .AddStackExchangeRedisOutputCache(options =>
    {
        options.InstanceName = configuration["Redis:Name"]!;
        options.Configuration = configuration["Redis:Url"];
    });

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.UseOutputCache();

app.MapLibraryMangaEndpoints();
app.MapUserMangaEndpoints();
app.MapOrderEndpoints();
app.MapUserEndpoints();
app.MapStatisticsEndpoints();

app.Run();