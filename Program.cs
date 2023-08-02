using MongoDB.Driver;
using Microsoft.Extensions.Configuration;



using API.Models;
using Microsoft.Extensions.Options;
using API.Services;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.Configure<DbContext>(
//     builder.Configuration.GetSection(nameof(MongoDB)));
    
builder.Services.AddApplicationServiceExtensions(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>(); //middleware for error handling.
// Configure the HTTP request pipeline.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
                       .WithOrigins("https://localhost:4200") );
 app.UseAuthentication(); //Middleware that tells : do you have a valid Token?
 app.UseAuthorization(); // Middleware that tells : yes I do, what are you allowed to do ?

app.MapControllers();

app.Run();
 