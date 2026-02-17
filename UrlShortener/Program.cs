using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;   
using UrlShortener.Data;
using UrlShortener.Models;
using UrlShortener.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register PostgreSQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<ShortCodeService>();

builder.Services.AddControllers();

builder.Services.AddSingleton<RedisService>();


var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Test endpoint
app.MapGet("/", () => "URL Shortener API Running");

app.MapControllers();

app.Run();
