using DAL.Repository.City;
using DAL.Repository.Person;
using DAL.Repository.PhoneNumbers;
using DAL.Repository.RelatedPersons;

namespace DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    public IPersonRepository PersonRepository { get; }
    public ICityRepository CityRepository { get; }
    public IRelatedPersonRepository RelatedPersonRepository { get; }
    public IPhoneNumberRepository PhoneNumberRepository { get; }
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveAsync();
}