using System.Data;

namespace DAL.Repository.PhoneNumbers;

public interface IPhoneNumberRepository
{
    Task AddAsync(Shared.Models.PhoneNumbers phoneNumber);
    Task<IEnumerable<Shared.Models.PhoneNumbers>> GetAllAsync();
    void SetTransaction(IDbTransaction transaction);
}