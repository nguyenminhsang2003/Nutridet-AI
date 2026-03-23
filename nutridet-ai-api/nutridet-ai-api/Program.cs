using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories;
using nutridet_ai_api.Repositories.IRepositories;
using nutridet_ai_api.Services;
using nutridet_ai_api.Services.IService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NutridetAiDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// register Jwt
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Add "Bearer : token" into swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Nhập token dạng: Bearer {your token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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
        policy.WithOrigins("http://localhost:53935")
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
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();


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
