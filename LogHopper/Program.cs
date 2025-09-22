using LogHopper.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TestConnection");

// if you use more than one db dont use this
builder.Services.AddSingleton(new MariaDBController(connectionString));


builder.Services.AddControllers();

// Add custom token authentication
builder.Services.AddAuthentication("TokenScheme")
    .AddScheme<AuthenticationSchemeOptions, TokenAuthHandler>("TokenScheme", null);

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
   // app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

app.Run();
