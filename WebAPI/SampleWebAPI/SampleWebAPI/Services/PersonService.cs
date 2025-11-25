using System;
using SampleWebAPI.Models;
using SampleWebAPI.Data.Repositories;

namespace SampleWebAPI.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person> AddAsync(Person person)
    {
        return await _personRepository.AddAsync(person);
    }

    public async Task DeleteAsync(int id)
    {
         await _personRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Person>> GetAllAysnc()
    {
        return await _personRepository.GetAllAysnc();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _personRepository.GetByIdAsync(id);
    }
}

