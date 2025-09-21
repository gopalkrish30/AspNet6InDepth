using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public ValuesController()
        {

        }

        [HttpGet]

        public IActionResult Get([FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var request = HttpContext.Request;

            var request2 = httpContextAccessor?.HttpContext?.Request;

            var response = httpContextAccessor?.HttpContext?.Response;

            ClaimsPrincipal? principal = httpContextAccessor?.HttpContext?.User;

            return Ok(request.Path + " " + request2.Path);
        }
    }
}
