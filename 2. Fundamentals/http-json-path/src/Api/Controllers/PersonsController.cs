using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public PersonsController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetPersonsPaginatedAsync([FromQuery] int page, [FromQuery] int limit)
    {
        return Ok(await _personRepository.GetPersonsPaginatedAsync(page, limit));   
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Person>> GetPersonByIdAsync([FromRoute] Guid id)
    {
        Person person = await _personRepository.GetPersonByIdAsync(id);

        if(person is null) return NotFound();   

        return Ok(person);
  
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PatchPersonAsync([FromRoute] Guid id, [FromBody] JsonPatchDocument<Person> patchperson)
    {
        Person person = await _personRepository.GetPersonByIdAsync(id);

        if (person is null) return NotFound();

        patchperson.ApplyTo(person);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Person updatedperson = await _personRepository.UpdatePersonAsync(id, person);

        return Ok(updatedperson);   
    }

}
