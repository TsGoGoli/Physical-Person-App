using System.Data;
using DAL.Repository.City;
using DAL.Repository.Person;
using DAL.Repository.PhoneNumbers;
using DAL.Repository.RelatedPersons;

namespace DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction = null!;
    private bool _disposed;

    public IPersonRepository PersonRepository { get; }
    public ICityRepository CityRepository { get; }
    public IRelatedPersonRepository RelatedPersonRepository { get; }
    public IPhoneNumberRepository PhoneNumberRepository { get; }


    public UnitOfWork(IDbConnection connection, IPersonRepository personRepository, ICityRepository cityRepository, IRelatedPersonRepository relatedPersonRepository, IPhoneNumberRepository phoneNumberRepository)
    {
        _connection = connection;
        _connection.Open();
        PersonRepository = personRepository;
        CityRepository = cityRepository;
        RelatedPersonRepository = relatedPersonRepository;
        PhoneNumberRepository = phoneNumberRepository;
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null!)
        {
            _transaction = await Task.Run(() => _connection.BeginTransaction());
            PersonRepository.SetTransaction(_transaction);
            CityRepository.SetTransaction(_transaction);
            PhoneNumberRepository.SetTransaction(_transaction);
            RelatedPersonRepository.SetTransaction(_transaction);
        }

    }

    public async Task CommitAsync()
    {
        if (_transaction != null!)
        {
            await Task.Run(() => _transaction.Commit());
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null!)
        {
            await Task.Run(() => _transaction.Rollback());
            await DisposeTransactionAsync();
        }
    }

    public async Task DisposeTransactionAsync()
    {
        await Task.Run(() =>
        {
            _transaction.Dispose();
            _transaction = null!;
        });
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            if (_transaction != null!)
            {
                await RollbackAsync();
            }
            _connection.Close();
            _connection.Dispose();
            _disposed = true;
        }
    }
    public async Task SaveAsync()
    {
        await CommitAsync();
    }
}