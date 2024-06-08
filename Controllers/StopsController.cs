using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using DominicoBus.DataTransfer;
using DominicoBus.Services;
using DominicoBus.Components.Tables;
using System.Collections.ObjectModel;

namespace DominicoBus.Controllers
{
    [Route("api/[controller]")]
    public class StopsController : Controller
    {
        private readonly StopService _service;
        public StopsController(StopService service)
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
            return new RazorComponentResult<StopsTable>(new { results = userResults });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            await _service.DeleteAsync(Id);
            return Ok();
        }
    }
}