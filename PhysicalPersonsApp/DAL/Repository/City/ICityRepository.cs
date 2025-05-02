namespace DAL.Repository.City;
using Shared.Models;
using System.Data;

public interface ICityRepository
{
    Task<City> GetByIdAsync(int id);
    Task AddAsync(City personCity);
    Task UpdateAsync(City updatedCity);
    Task DeleteAsync(int id);
    Task<IEnumerable<City>> GetAllAsync();
    void SetTransaction(IDbTransaction transaction);
}