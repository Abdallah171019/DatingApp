using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly UserService _userService;
        public BuggyController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound(string id)
        {
            var thing = _userService.Get(id);
            if (thing == null) return NotFound();
            return NotFound();
        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError(string id)
        {
            var thing = _userService.Get(id);
            thing = null;
            var thingToReturn = thing.ToString();
            return thingToReturn;

        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request !");
        }

    }


}