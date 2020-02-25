using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PwcBios.Api.CQRS.Queries;

namespace PwcBios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusQuery _statusQuery;

        public StatusController(IStatusQuery statusQuery)
        {
            this._statusQuery = statusQuery;
        }
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            //var result = await _statusQuery.GetStatusAsync(CancellationToken.None)? "Hellow World": "Not connected";
            //return Ok(result);
            throw new System.Exception("Exception here!");
        }
    }
}