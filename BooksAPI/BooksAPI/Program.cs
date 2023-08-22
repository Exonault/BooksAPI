using AutoMapper;
using BooksAPI.Data;
using BooksAPI.Endpoints;
using BooksAPI.Mapping;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

MapperConfiguration mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new OrderProfile());
    config.AddProfile(new ComicProfile());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddComicServices();
builder.Services.AddOrderServices();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationDb"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapComicEndpoints();
app.MapOrderEndpoints();


app.Run();