using Microsoft.EntityFrameworkCore;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories;
using nutridet_ai_api.Repositories.IRepositories;
using nutridet_ai_api.Services;
using nutridet_ai_api.Services.IService;

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

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:57450")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Register Repositories (Repositories Layer)
builder.Services.AddScoped<IScanImageRepository, ScanImageRepository>();
builder.Services.AddScoped<IOutputNutritionRepository, OutputNutritionRepository>();
builder.Services.AddScoped<INutritionVisualRuleReponsitory, NutritionVisualRuleReponsitory>();
builder.Services.AddScoped<IOutputNutritionVisualReponsitory, OutputNutritionVisualReponsitory>();
builder.Services.AddScoped<INutritionExcerciseRuleReponsitory, NutritionExcerciseRuleReponsitory>();
builder.Services.AddScoped<IOutputNutritionExcerciseReponsitory, OutputNutritionExcerciseReponsitory>();
builder.Services.AddScoped<IUserReponsitory, UserReponsitory>();

// Register Services (Service Layer)
builder.Services.AddScoped<IScanImageService, ScanImageService>();
builder.Services.AddScoped<IGeminiService, GeminiService>();
builder.Services.AddScoped<IOutputNutritionVisualService, OutputNutritionVisualService>();
builder.Services.AddScoped<IOutputNutritionExcerciseService, OutputNutritionExcerciseService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
