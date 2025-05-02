using DAL.UnitOfWork;
using Shared.Models;
using Shared.Models.Dto;

namespace BAL.Services.RelatedPerson;

public class RelatedPersonService(IUnitOfWork unitOfWork) : IRelatedPersonService
{
    public async Task AddRelation(AddRelationDto relationDto)
    {
        var relation = new RelatedPersons
        {
            RelatedPersonId = relationDto.RelatedPersonId,
            PersonId = relationDto.PersonId,
            RelationshipType = relationDto.RelationShipType
        };

        try
        {
            await unitOfWork.BeginTransactionAsync();
            var fromPerson = await unitOfWork.PersonRepository.GetByIdAsync(relationDto.PersonId);
            var toPerson = await unitOfWork.PersonRepository.GetByIdAsync(relationDto.RelatedPersonId);

            if (fromPerson != null && toPerson != null)
            {
                await unitOfWork.RelatedPersonRepository.AddAsync(relation);
                await unitOfWork.CommitAsync();
            }
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackAsync();
        }

        
    }


    public async Task DeleteByIdsAsync(int personId, int relatedPersonId)
    {
        await unitOfWork.RelatedPersonRepository.DeleteByIdsAsync(personId, relatedPersonId);
        await unitOfWork.SaveAsync();
    }
}