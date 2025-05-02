using System.Data;
using Dapper;

namespace DAL.Repository.RelatedPersons;

public class SqlRelatedPersonRepositoryDapper : IRelatedPersonRepository
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction = null!;


    public SqlRelatedPersonRepositoryDapper(IDbConnection connection)
    {
        _connection = connection;
    }
    public void SetTransaction(IDbTransaction transaction)
    {
        _transaction = transaction;
    }

    public async Task AddAsync(Shared.Models.RelatedPersons relatedPersons)
    {
        var query =
            "INSERT INTO RelatedPersons (PersonID, RelatedPersonID, RelationshipType) VALUES (@PersonID, @RelatedPersonID, @RelationshipType)";

        await _connection.ExecuteAsync(query, relatedPersons, _transaction);
    }

    public async Task<IEnumerable<Shared.Models.RelatedPersons>> GetAllAsync()
    {
        var query = "SELECT * FROM RelatedPersons";

        return await _connection.QueryAsync<Shared.Models.RelatedPersons>(query, _transaction);
    }

    public async Task DeleteAsync(int id)
    {
        const string query = "DELETE FROM RelatedPersons WHERE ID = @ID";

        await _connection.ExecuteAsync(query, new { ID = id });
    }

    public async Task DeleteByIdsAsync(int personId, int relatedPersonId)
    {
        const string query = "DELETE FROM RelatedPersons WHERE PersonID = @PersonID AND RelatedPersonID = @RelatedPersonID";

        await _connection.ExecuteAsync(query, new { PersonID = personId, RelatedPersonID = relatedPersonId });
    }
}