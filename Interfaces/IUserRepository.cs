using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
    Task<List<AppUser>> GetAsync();
    Task<AppUser> GetAsyncUser(string username);
    Task<bool> CheckDbContent();
    AppUser Get(string id);
    Task<AppUser> GetAsyncId(string id);
    Task<IEnumerable<MembersDto>> GetAsyncMembers();
    Task<MembersDto> GetAsyncMember(string username);
    Task CreateAsync(AppUser user);
    Task UpdateAsync(string id, AppUser user);
    Task RemoveAsync(string id);


    }
}