using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using API.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.JsonData
{
    public class Seed
    {
        public static async Task SeedUsers(UserService userService)
        {
            if (await userService.CheckDbContent()) return;
            var userData = await File.ReadAllTextAsync("JsonData/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;
                await userService.CreateAsync(user);
            }
        }
        
        public async Task<bool> UserExist(UserService userService, string username){
            var user =  await userService.GetAsyncUser(username.ToLower());
            return user != null;
        }
    }
}