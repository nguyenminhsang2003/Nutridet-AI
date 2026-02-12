using Microsoft.EntityFrameworkCore;
using nutridet_ai_api.Models;
using nutridet_ai_api.Services;
using nutridet_ai_api.Services.IService;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NutridetAiDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

// Register Services (Service Layer)
builder.Services.AddScoped<IScanImageService, ScanImageService>();
builder.Services.AddScoped<IGeminiService, GeminiService>();



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
