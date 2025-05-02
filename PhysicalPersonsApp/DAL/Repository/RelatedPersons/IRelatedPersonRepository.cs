using System.Data;

namespace DAL.Repository.RelatedPersons;

public interface IRelatedPersonRepository
{
    Task AddAsync(Shared.Models.RelatedPersons relatedPersons);
    Task DeleteAsync(int id);
    Task DeleteByIdsAsync(int personId, int relatedPersonId);
    Task<IEnumerable<Shared.Models.RelatedPersons>> GetAllAsync();
    void SetTransaction(IDbTransaction transaction);
}