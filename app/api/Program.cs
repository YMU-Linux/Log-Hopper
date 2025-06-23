using System.ComponentModel;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseAuthentication();
//app.UseAuthorization();
//app.UseHttpsRedirection();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", () => "Welcome to the Log-Hopper API!");
    endpoints.MapGet("/api/health", () => Results.Ok("API is running!"));
});

app.UseRouting();

app.Run();