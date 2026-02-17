using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;   
using UrlShortener.Data;
using UrlShortener.Models;
using UrlShortener.Services;
using UrlShortener.Data.Shards;


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

builder.Services.AddDbContext<Shard1DbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Shard1")));

builder.Services.AddDbContext<Shard2DbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Shard2")));


builder.Services.AddScoped<ShortCodeService>();
builder.Services.AddSingleton<ShardRouter>();

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
