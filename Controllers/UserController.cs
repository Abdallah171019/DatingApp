
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }
        //GET 
       [HttpGet]
        public async Task<List<AppUser>> Get() =>
        await userService.GetAsync();

        [HttpGet("{id}")]
       
   
         public async Task<ActionResult<AppUser>> Get(string id)
         {
             var user = await userService.GetAsync(id);

            if (user is null)
             {
                 return NotFound();
             }

             return user;
        }
       [HttpPost]
        public async Task<IActionResult> Post(AppUser newUser)
        {
            await userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, AppUser updatedUser)
      {
        var user = await userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.Id = user.Id;

        await userService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

        [HttpDelete("{id}")]
         public async Task<IActionResult> Delete(string id)
    {
        var user = await userService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await userService.RemoveAsync(id);

        return NoContent();
    }

    }
}