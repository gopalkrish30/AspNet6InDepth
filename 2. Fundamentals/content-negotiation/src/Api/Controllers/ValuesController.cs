using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        List<Item> list = new List<Item>
        {
            new Item
            {
                Id = Guid.NewGuid(),
                Name = "Television",
                price = "25,000"
            },
            new Item {
                Id = Guid.NewGuid(),
                Name = "Iphone",
                price = "60,000"
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(list);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult Get2(Guid id)
        {
            Item item = list.FirstOrDefault(x => x.Id == id);   
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<List<Item>> Post([FromBody] Item item)
        {
            list.Add(item);
            return CreatedAtAction(nameof(Get2), new { id = item.Id }, item);
        }
    }
}
