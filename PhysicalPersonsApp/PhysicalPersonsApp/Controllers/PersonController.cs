using BAL.Services.Person;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto;

namespace PhyisicalPersonsApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private IPersonService _personService;
    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }


    [HttpPost]
    public async Task<IActionResult> AddPerson([FromForm]AddPersonDto personDto)
    {
        await _personService.AddPersonAsync(personDto);
        return Ok(new {Message = "Person added!" });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePerson(UpdatePersonDto personDto, int id)
    {
        await _personService.UpdatePersonAsync(personDto, id);
        return Ok(new { Message = "Person updated!" });
    }


    [HttpGet]
    public async Task<IActionResult> GetPersonById(int id)
    {
        var result = await _personService.GetPersonById(id);
        return Ok(result);
    }
    [HttpGet("Search")]
    public async Task<IActionResult> SearchPersonByWord([FromQuery] string searchWord, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
    {
        var response = await _personService.GetByFields(searchWord, pageNumber, pageSize);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        await _personService.DeletePersonAsync(id);
        return Ok(new { Message = "Person deleted successfully" });
    }
}
