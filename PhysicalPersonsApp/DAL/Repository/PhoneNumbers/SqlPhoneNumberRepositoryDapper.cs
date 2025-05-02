using System.Data;
using Dapper;

namespace DAL.Repository.PhoneNumbers;

public class SqlPhoneNumberRepositoryDapper : IPhoneNumberRepository
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction = null!;


    public SqlPhoneNumberRepositoryDapper(IDbConnection connection)
    {
        _connection = connection;
    }
    public void SetTransaction(IDbTransaction transaction)
    {
        _transaction = transaction;
    }


    public async Task AddAsync(Shared.Models.PhoneNumbers phoneNumber)
    {
        var query =
            "INSERT INTO PhoneNumbers (PhoneNumberID, PersonID, PhoneType, PhoneNumber) VALUES (@PhoneNumberID, @PersonID, @PhoneType, @PhoneNumber)";

        await _connection.ExecuteAsync(query, phoneNumber, _transaction);
    }

    public async Task<IEnumerable<Shared.Models.PhoneNumbers>> GetAllAsync()
    {
        var query = "SELECT * FROM PhoneNumbers";

        return await _connection.QueryAsync<Shared.Models.PhoneNumbers>(query, _transaction);
    }
}