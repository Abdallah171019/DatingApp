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
using API.JsonData;

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

using var scope = app.Services.CreateScope(); // this allows us to access to all the services that we have inside this program class.
var services = scope.ServiceProvider;
try{
    var userService = services.GetRequiredService<UserService>();
    await Seed.SeedUsers(userService);

}
catch(Exception ex){
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during seeding"); 
}
app.Run();
 