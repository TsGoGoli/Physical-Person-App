using Dapper;

namespace DAL.Repository.City;
using Shared.Models;
using System.Data;

public class SqlCityRepositoryDapper : ICityRepository
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction = null!;

    public SqlCityRepositoryDapper(IDbConnection connection)
    {
        _connection = connection;
    }
    public void SetTransaction(IDbTransaction transaction)
    {
        _transaction = transaction;
    }
    public async Task<City> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM Cities WHERE CityID = @ID";

        return await _connection.QueryFirstAsync<City>(query, new { ID = id }, _transaction);
    }

    public async Task AddAsync(City city)
    {
        var query = "INSERT INTO Cities (CityName) VALUES (@CityName)";

        await _connection.ExecuteAsync(query, city, _transaction);
    }

    public async Task UpdateAsync(City city)
    {
        var query = "UPDATE Cities SET CityName = @CityName WHERE CityID = @Id";

        await _connection.ExecuteAsync(query, city, _transaction);
    }

    public async Task DeleteAsync(int id)
    {
        var query = "DELETE FROM Cities WHERE CityID = @id";

        await _connection.ExecuteAsync(query, id, _transaction);
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        var query = "SELECT * FROM Cities";

        return await _connection.QueryAsync<City>(query, _transaction);
    }
}