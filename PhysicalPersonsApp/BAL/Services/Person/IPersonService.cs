using Shared.Models;
using Shared.Models.Dto;

namespace BAL.Services.Person;

public interface IPersonService
{
    Task AddPersonAsync(AddPersonDto person);
    Task UpdatePersonAsync(UpdatePersonDto person, int id);
    Task<Shared.Models.Person> GetPersonById(int id);
    Task DeletePersonAsync(int id);
    Task<PaginatedResponse<Shared.Models.Person>> GetByFields(string searchInput, int pageNumber = 1, int pageSize = 5);
}
