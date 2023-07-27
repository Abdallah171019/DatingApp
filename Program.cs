using MongoDB.Driver;
using Microsoft.Extensions.Configuration;



using API.Models;
using Microsoft.Extensions.Options;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.Configure<DbContext>(
//     builder.Configuration.GetSection(nameof(MongoDB)));
    
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<UserService>();
builder.Services.AddCors();


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200") );

 
app.MapControllers();

app.Run();
