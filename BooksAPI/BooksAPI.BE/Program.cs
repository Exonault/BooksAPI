using System.Security.Claims;
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

//Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

//DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("ApplicationDb1"));
});



//Auth
builder.Services.AddIdentity<User, IdentityRole>()
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AppConstants.AdminRolePolicyName, p =>
    {
        p.RequireClaim(ClaimTypes.Role, "Admin");
    });
});

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigin",
        policy =>
        {
            policy.WithOrigins(configuration.GetSection("FrontEndUrl").Value!);
        });
});

//Entity Services
builder.Services.AddLibraryComicServices();
builder.Services.AddUserServices();

//Mapping
MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
{
    config.AddProfile(new LibraryComicProfile());
});

builder.Services.AddSingleton(mapperConfiguration.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowedOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapLibraryComicEndpoints();
app.MapUserEndpoints();

app.Run();