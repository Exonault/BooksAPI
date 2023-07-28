using AutoMapper;
using BooksAPI.Data;
using BooksAPI.Entities;
using BooksAPI.Interfaces.Repositories;
using BooksAPI.Interfaces.Services;
using BooksAPI.Mapping;
using BooksAPI.Repositories;
using BooksAPI.Services;
using BooksAPI.Validation;
using FluentValidation;
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

builder.Services.AddScoped<IComicRepository, ComicRepository>();
builder.Services.AddScoped<IComicService, ComicService>();
builder.Services.AddScoped<IValidator<Comic>, ComicValidator>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IValidator<Order>, OrderValidator>();


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();