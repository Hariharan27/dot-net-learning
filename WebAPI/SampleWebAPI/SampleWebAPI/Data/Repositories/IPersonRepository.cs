using System;
using SampleWebAPI.Models;
namespace SampleWebAPI.Data.Repositories;

public interface IPersonRepository
{
	Task<IEnumerable<Person>> GetAllAysnc();
    Task<Person?> GetByIdAsync(int id);
    Task<Person> AddAsync(Person person);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}

