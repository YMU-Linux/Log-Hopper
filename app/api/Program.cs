using System.ComponentModel;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using api.Modules;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("your_super_secret_key"))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Optional if you want Swagger UI

var app = builder.Build();

// Middleware order matters!
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Swagger (optional)
app.UseSwagger();
app.UseSwaggerUI();

// Minimal API endpoints
app.MapGet("/", () => "Welcome to the Log-Hopper API!");

app.MapGet("/api/health", () => Results.Ok("API is running!"));

// Use POST if you want to read request body
app.MapPost("/api/login", (LoginRequest data, HttpRequest request) =>
{
    var userAgent = request.Headers["User-Agent"].ToString();

    return Results.Ok(new
    {
        Message = "Login received!",
        Username = data.Username,
        Password = data.Password,
        UserAgent = userAgent
    });
});


app.Run();
