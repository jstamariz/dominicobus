using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using DominicoBus.Components.Tables;
using DominicoBus.Services;

namespace DominicoBus.Controllers
{
    [Route("api/[controller]")]
    public class BusesController : Controller
    {
        private readonly BusService _service;

        public BusesController(BusService service)
        {
            _service = service;
        }

        [HttpPost("search")]
        public IResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return new RazorComponentResult<BusTable>();
            }

            var userResults = _service.Search(search);
            return new RazorComponentResult<BusTable>(new { results = userResults });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            await _service.DeleteAsync(Id);
            return Ok();
        }
    }
}