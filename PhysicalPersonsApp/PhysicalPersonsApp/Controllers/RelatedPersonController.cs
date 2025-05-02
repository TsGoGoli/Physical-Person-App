using BAL.Services.RelatedPerson;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto;

namespace PhyisicalPersonsApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RelatedPersonController : ControllerBase
{
    private readonly IRelatedPersonService _relatedPersonService;
    public RelatedPersonController(IRelatedPersonService relatedPersonService)
    {
        _relatedPersonService = relatedPersonService;
    }

    [HttpPost]
    public async Task<IActionResult> AddRelation(AddRelationDto relationDto)
    {
        await _relatedPersonService.AddRelation(relationDto);
        return Ok(new { Message = "Related added" });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRelation(DeletePersonRelationDto relationDto)
    {
        await _relatedPersonService.DeleteByIdsAsync(relationDto.PersonId, relationDto.RelatedPersonId);

        return Ok(new { Message = "Relation successfully deleted!" });
    }
}
