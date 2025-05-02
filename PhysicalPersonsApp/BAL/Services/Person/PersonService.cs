using DAL.UnitOfWork;
using Shared.Models;
using Shared.Models.Dto;
namespace BAL.Services.Person;

public class PersonService: IPersonService
{
    private readonly IUnitOfWork _unitOfWork;
    public PersonService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task AddPersonAsync(AddPersonDto personDto)
    {
        var person = new Shared.Models.Person
        {
            Name = personDto.Name,
            Surname = personDto.Surname,
            Gender = personDto.Gender,
            PersonalNumber = personDto.PersonalNumber,
            CityId = personDto.CityID,
            BirthDate = personDto.BirthDate,
            Image = personDto.Image.FileName
        };
       
        await _unitOfWork.PersonRepository.AddAsync(person);
    }

    public async Task UpdatePersonAsync(UpdatePersonDto personDto, int id)
    {
        var person = new Shared.Models.Person
        {
            PersonId = id,
            Name = personDto.Name,
            Surname = personDto.Surname,
            Gender = personDto.Gender,
            PersonalNumber = personDto.PersonalNumber,
            CityId = personDto.CityID,
            BirthDate = personDto.BirthDate,
            Image = personDto.Image.FileName
        };

        await _unitOfWork.PersonRepository.UpdateAsync(person);
    }

    public async Task<Shared.Models.Person> GetPersonById(int id)
    {
        return await _unitOfWork.PersonRepository.GetByIdAsync(id);
    }
    public async Task<PaginatedResponse<Shared.Models.Person>> GetByFields(string searchInput, int pageNumber = 1, int pageSize = 5)
    {
        var persons = await _unitOfWork.PersonRepository.GetByFields(searchInput);

        var enumerable = persons.ToList();

        var totalRecords = enumerable.Count();

        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

        if (pageNumber < 1)
            pageNumber = 1;

        if (pageNumber > totalPages)
            pageNumber = totalPages;

        var personsResult = enumerable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        var response = new PaginatedResponse<Shared.Models.Person>
        {
            CurrentPage = pageNumber,
            TotalPages = totalPages,
            TotalRecords = totalRecords,
            Items = personsResult
        };


        return response;
    }

    public async Task DeletePersonAsync(int id)
    {
        await _unitOfWork.PersonRepository.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
    }
}
