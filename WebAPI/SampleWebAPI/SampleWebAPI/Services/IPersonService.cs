using System;
using SampleWebAPI.Models;

namespace SampleWebAPI.Services;

public interface IPersonService
{
    Task<IEnumerable<Person>> GetAllAysnc();
    Task<Person?> GetByIdAsync(int id);
    Task<Person> AddAsync(Person person);
    Task DeleteAsync(int id);
}

