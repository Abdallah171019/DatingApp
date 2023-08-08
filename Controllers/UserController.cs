
using System.Security.Claims;
using API.DTO;
using API.Entities;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [Authorize]
    public class UserController : BaseApiController
    {
        private readonly UserService userService;
        private readonly IMapper mapper;

        public UserController(UserService userService , IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }
    
        //GET 
       [HttpGet]
        public async Task<ActionResult<List<MembersDTO>>> Get() {
         /* 
         var user = await userService.GetAsync();
         var userMappeds = mapper.Map<List<MembersDto>>(user);
    
         return Ok(userMappeds); */
         var user = await userService.GetAsyncMembers();
         return Ok(user);

        }
        
        
/*         [HttpGet("{id}")]
         public async Task<ActionResult<AppUser>> Get(string id)
         {
             var user = await userService.GetAsyncId(id);

            if (user is null)
             {
                 return NotFound();
             }

             return user;
        } */
        
        [HttpGet("{username}")]
         public async Task<ActionResult<MembersDTO>> GetByUsername(string username)
         {
            /* var user = await userService.GetAsyncUser(username);
            var userMapped = mapper.Map<MembersDto>(user);*/
            var user = await userService.GetAsyncMember(username);

             return Ok(user);
        }
        
       
       [HttpPost]
        public async Task<IActionResult> Post(AppUser newUser)
        {
            await userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }
/* 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, AppUser updatedUser)
      {
        var user = await userService.GetAsyncId(id);

        if (user is null)
        {
            return NotFound();
        }
        updatedUser.Id = user.Id;

        await userService.UpdateAsync(id, updatedUser);

        return Ok(updatedUser);
    } */

        [HttpPut]
        public async Task<IActionResult> Update(MemberUpdatedDTO memberUpdatedDTO)
      {
      
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await userService.GetAsyncUser(username);
        if (user == null) return NotFound();
        mapper.Map(memberUpdatedDTO,user);
        if (await userService.SaveAllAsync(user)) return NoContent();
        return BadRequest("Failed to update user");
    } 


        


        [HttpDelete("{id}")]
         public async Task<IActionResult> Delete(string id)
    {
        var user = await userService.GetAsyncId(id);

        if (user is null)
        {
            return NotFound();
        }

        await userService.RemoveAsync(id);

        return NoContent();
    }

    }
}