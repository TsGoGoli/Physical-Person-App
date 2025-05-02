namespace DAL.Repository.Person;
using Shared.Models;
using System.Data;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(int id);
    Task AddAsync(Person person);
    Task UpdateAsync(Person updatedPerson);
    Task<IEnumerable<Person>> GetByFields(string searchInput);
    Task DeleteAsync(int id);
    Task<IEnumerable<Person>> GetAllAsync();
    void SetTransaction(IDbTransaction transaction);
}
