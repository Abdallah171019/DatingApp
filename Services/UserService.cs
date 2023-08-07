using API.DTO;
using API.Entities;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API.Services;

public class UserService : IUserRepository
{
    private readonly IMongoCollection<AppUser> _user;
        public IMapper _mapper { get; }

    public UserService(
        IOptions<DatabaseSettings> datingDatabaseSettings , IMapper mapper)
    {
            _mapper = mapper;
        var mongoClient = new MongoClient(
            datingDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            datingDatabaseSettings.Value.DatabaseName);

        _user = mongoDatabase.GetCollection<AppUser>(
            datingDatabaseSettings.Value.CollectionName);
    }
    public async Task<List<AppUser>> GetAsync() =>
        await _user.Find(_ => true).ToListAsync();
   public async Task<bool> CheckDbContent()
    {
        long count = await _user.CountDocumentsAsync(FilterDefinition<AppUser>.Empty);
        return count > 0;
    }
    public ActionResult<AppUser> Get(string id) => 
    _user.Find(x => x.Id == id).FirstOrDefault();

    public async Task<AppUser> GetAsyncId(string id) => 
    await _user.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<AppUser> GetAsyncUser(string username) => 
    await _user.Find(x => x.UserName == username).FirstOrDefaultAsync();
    
    public async Task CreateAsync(AppUser user) =>
        await _user.InsertOneAsync(user);

    public async Task UpdateAsync(string id, AppUser Username) =>
        await _user.ReplaceOneAsync(x => x.Id == id, Username);

    public async Task RemoveAsync(string id) =>
        await _user.DeleteOneAsync(x => x.Id == id);

    internal Task<bool> GetAsync(Func<object, object> value, object user)
    {
        throw new NotImplementedException();
    }

    AppUser IUserRepository.Get(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MembersDto>> GetAsyncMembers() 
    {
    
        var user= await _user.Find(_ => true).ToListAsync();
        var userMappeds = _mapper.Map<IEnumerable<MembersDto>>(user);
        return userMappeds;
        
    }

    public async Task<MembersDto> GetAsyncMember(string username)
    {
        
           var user = await _user.Find(x => x.UserName == username).SingleOrDefaultAsync();
           var mappedUsers = _mapper.Map<MembersDto>(user);
           return mappedUsers;
    }

    

}