using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserService _user;
        public   ITokenService _tokenService { get; }
        public AccountController(UserService userService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _user = userService;
            
        }
   
        [HttpPost("register")] //Post : api/Account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){

            if (await UserExist(registerDto.Username)) return BadRequest("Username is taken!"); //400
            using var hmac = new HMACSHA512();

            var user = new AppUser {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                PasswordSalt = hmac.Key
            };
             await _user.CreateAsync(user);
             return new UserDto {
                Username = user.UserName,
                Token    = _tokenService.CreateToken(user)
             };

            
        }
        public async Task<bool> UserExist(string username){
            var user =  await _user.GetAsyncUser(username.ToLower());
            return user != null;
        }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){

        var user = await _user.GetAsyncUser(loginDto.Username); 
        
        if (user == null) return Unauthorized("Invalid username!"); //401
        using var hmac = new HMACSHA512(user.PasswordSalt); //password key to use the same algo when typing the same password
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));
        for(int i =0; i < computedHash.Length; i++) {
            if(computedHash[i] != user.PasswordHash[i] ) return Unauthorized("Invalid Password");
        }
        return new UserDto{
            Username = user.UserName,
            Token    = _tokenService.CreateToken(user)
        };
    }
        
    }
}