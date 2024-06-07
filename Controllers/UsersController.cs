using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using DominicoBus.DataTransfer;
using DominicoBus.Services;
using DominicoBus.Components.Tables;
using System.Collections.ObjectModel;

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

        [HttpPost("search")]
        public IResult Search(string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                return new RazorComponentResult<UsersTable>();
            }

            var userResults = _userService.Search(search);
            return new RazorComponentResult<UsersTable>(new { results = userResults });
        }

    }
}