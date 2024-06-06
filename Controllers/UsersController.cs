using Microsoft.AspNetCore.Mvc;
using DominicoBus.DataTransfer;
using DominicoBus.Services;

namespace DominicoBus.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        public UsersController(UserService service)
        {
            _userService = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO user)
        {
            if (ModelState.IsValid)
            {
                await _userService.Create(user);
                return Created();
            }
            return BadRequest();
        }

    }
}