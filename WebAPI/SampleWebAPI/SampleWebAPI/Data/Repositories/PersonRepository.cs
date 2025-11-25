using System;
using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data.Repositories;
using SampleWebAPI.Models;

namespace SampleWebAPI.Data.Repositories
{
	public class PersonRepository : IPersonRepository
	{
        private readonly AppDbContext _context;


        public PersonRepository(AppDbContext context)
		{
            _context = context;
		}

        public async Task<Person> AddAsync(Person person)
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Persons.FindAsync(id);
            if (entity != null)
            {
                _context.Persons.Remove(entity);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Person>> GetAllAysnc()
        {
            return await _context.Persons.AsNoTracking().ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
             return await _context.Persons.FindAsync(id); ;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

