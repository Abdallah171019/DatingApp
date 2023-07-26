using API.Entities;
using API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Services;

public class UserService
{
    private readonly IMongoCollection<AppUser> _user;

    public UserService(
        IOptions<DatabaseSettings> datingDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            datingDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            datingDatabaseSettings.Value.DatabaseName);

        _user = mongoDatabase.GetCollection<AppUser>(
            datingDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<AppUser>> GetAsync() =>
        await _user.Find(_ => true).ToListAsync();

    public async Task<AppUser> GetAsync(string id) =>
        await _user.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(AppUser user) =>
        await _user.InsertOneAsync(user);

    public async Task UpdateAsync(string id, AppUser Username) =>
        await _user.ReplaceOneAsync(x => x.Id == id, Username);

    public async Task RemoveAsync(string id) =>
        await _user.DeleteOneAsync(x => x.Id == id);
}