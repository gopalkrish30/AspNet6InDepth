using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet("GetValues/{val1:int}/GetMoreValues/{val2:double}")]
        public IActionResult Get(
            [FromRoute] int val1,
            [FromRoute] double val2,

            [FromQuery] string q1,
            [FromQuery] string q2
            )
        {
            return Ok();
        }


        [HttpGet("")]
        [HttpGet("Index")]

        public ActionResult<String> Get()
        {
            return Ok("From Default Route");
        }


        [HttpGet("GetValuesfromdto/{val1:int}/GetMoreValues/{val2:double}")]
        public IActionResult Get(
            [FromRoute] RouteParamsDto dto,
            [FromQuery] QueryParam query_dto
            )
        {
            var response = new
            {
                route1 = dto.value1,
                route2 = dto.value2,
                query_dto = query_dto.q1,
                query_dto2 = query_dto.q2
            };
            return Ok(response);
        }


        [HttpPost]
        public IActionResult Post([FromBody] BodyDto request)
        {
            return Ok(request);
        }

        [HttpPost("manual")]
        public IActionResult Post()
        {
            byte[] b = new byte[byte.MaxValue];

            HttpContext.Request.Body.ReadAsync(b, 0, b.Length);

            string request = Encoding.UTF8.GetString(b).Replace("\0", string.Empty);

            var dto = JsonSerializer.Deserialize<BodyDto>(request);

            return Ok(dto);
        }

        [HttpPost("form")]

        public IActionResult Post2([FromForm] BodyDto dto)
        {
            return Ok(dto);
        }

        [HttpPost("header")]

        public IActionResult Post3([FromHeader] string customheader)
        {
            return Ok(customheader);
        }
    }
}
