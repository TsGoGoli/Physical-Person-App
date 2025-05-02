using Shared.Models.Dto;

namespace BAL.Services.RelatedPerson;

public interface IRelatedPersonService
{
    Task AddRelation(AddRelationDto relationDto);
    Task DeleteByIdsAsync(int personId, int relatedPersonId);
}