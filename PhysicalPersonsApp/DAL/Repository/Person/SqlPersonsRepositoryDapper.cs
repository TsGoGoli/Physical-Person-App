using Microsoft.IdentityModel.Tokens;

namespace DAL.Repository.Person;
using Shared.Models;
using Dapper;
using System;
using System.Data;

public class SqlPersonsRepositoryDapper : IPersonRepository
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction = null!;


    public SqlPersonsRepositoryDapper(IDbConnection connection)
    {
        _connection = connection;
    }
    public void SetTransaction(IDbTransaction transaction)
    {
        _transaction = transaction;
    }

    public async Task AddAsync(Person person)
    {
        var query = @"INSERT INTO Persons(Name, Surname, Gender, PersonalNumber, CityID, Image, BirthDate) 
                VALUES (@Name, @Surname, @Gender, @PersonalNumber, @CityID, @Image, @BirthDate)";


        await _connection.ExecuteAsync(query, person, _transaction);
    }

    public async Task DeleteAsync(int id)
    {
        var query = "DELETE FROM Persons WHERE ID = @ID";


        await _connection.ExecuteAsync(query, new {ID = id}, _transaction);
    }

    public Task<IEnumerable<Person>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        const string query = @"
                                SELECT 
                                    p.ID AS PersonId, p.Name, p.Surname, p.Gender, p.PersonalNumber, p.BirthDate, p.Image,
                                    c.CityID AS CityId, c.CityName AS CityName,
                                    ph.PhoneNumberID AS PhoneNumberId, ph.PhoneNumber, ph.PhoneType, ph.PersonId AS PersonId,
                                    rp.RelatedPersonID AS RelatedPersonId, rp.PersonID AS PersonId, rp.RelationshipType
                                FROM Persons p
                                LEFT JOIN Cities c ON p.CityId = c.CityID
                                LEFT JOIN PhoneNumbers ph ON p.ID = ph.PersonId
                                LEFT JOIN RelatedPersons rp ON p.ID = rp.RelatedPersonID
                                WHERE p.Id = @ID;";

        var person =
            await _connection.QueryAsync<Person, City, PhoneNumbers, RelatedPersons,
                Person>(query, (person, city, phoneNumber, relatedPerson) =>
                {
                    var currentPerson = person;
                    currentPerson.City = city;
                    currentPerson.PhoneNumbers = new List<PhoneNumbers>();
                    currentPerson.RelatedPersons = new List<RelatedPersons>();

                    if (phoneNumber != null!)
                    {
                        currentPerson.PhoneNumbers.Add(phoneNumber);
                    }

                    if (relatedPerson != null!)
                    {
                        if (currentPerson.RelatedPersons.All(rp => rp.RelatedPersonId != relatedPerson.RelatedPersonId))
                        {
                            currentPerson.RelatedPersons.Add(relatedPerson);
                        }
                    }

                    return currentPerson;
                },
                new { ID = id },
                splitOn: "CityID,PhoneNumberID,RelatedPersonID",
                transaction: _transaction);

        return person.IsNullOrEmpty() ? null : person.First();
    }

    public async Task UpdateAsync(Person updatedPerson)
    {
        var query =
            "UPDATE Persons SET Name = @Name, Surname = @Surname, Gender = @Gender, PersonalNumber = @PersonalNumber, CityID = @CityID, Image = @Image, BirthDate= @BirthDate WHERE ID = @PersonId";

        await _connection.ExecuteAsync(query, updatedPerson, _transaction);
    }
    public async Task<IEnumerable<Person>> GetByFields(string searchInput)
    {
        const string query = @"
            SELECT 
                p.ID AS PersonId, p.Name, p.Surname, p.Gender, p.PersonalNumber, p.BirthDate, p.Image,
                c.CityID AS CityId, c.CityName AS CityName,
                ph.PhoneNumberID AS PhoneNumberId, ph.PhoneNumber, ph.PhoneType, ph.PersonId AS PersonId,
                rp.RelatedPersonID AS RelatedPersonId, rp.PersonID AS PersonId, rp.RelationshipType
            FROM Persons p
            LEFT JOIN Cities c ON p.CityId = c.CityID
            LEFT JOIN PhoneNumbers ph ON p.ID = ph.PersonId
            LEFT JOIN RelatedPersons rp ON p.ID = rp.RelatedPersonID
            WHERE p.Name LIKE @SearchPattern OR p.Surname LIKE @SearchPattern OR p.PersonalNumber LIKE @SearchPattern;";

        var personDictionary = new Dictionary<int, Person>();

        var persons = await _connection.QueryAsync<Person, City, PhoneNumbers, RelatedPersons, Person>(
            query,
            (person, city, phoneNumber, relatedPerson) =>
            {
                if (!personDictionary.TryGetValue(person.PersonId, out var currentPerson))
                {
                    currentPerson = person;
                    currentPerson.City = city;
                    currentPerson.PhoneNumbers = new List<PhoneNumbers>();
                    currentPerson.RelatedPersons = new List<RelatedPersons>();
                    personDictionary.Add(currentPerson.PersonId, currentPerson);
                }

                if (phoneNumber != null! && currentPerson.PhoneNumbers.All(p => p.PersonId != phoneNumber.PersonId))
                {
                    currentPerson.PhoneNumbers.Add(phoneNumber);
                }

                if (relatedPerson != null! && currentPerson.RelatedPersons.All(r => r.RelatedPersonId != relatedPerson.RelatedPersonId))
                {
                    currentPerson.RelatedPersons.Add(relatedPerson);
                }

                return currentPerson;
            },
            new { SearchPattern = $"%{searchInput}%" },
            splitOn: "CityId,PhoneNumberId,RelatedPersonId");

        return personDictionary.Values;
    }
}
