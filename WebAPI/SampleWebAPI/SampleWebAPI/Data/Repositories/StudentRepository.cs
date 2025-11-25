using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Models;

namespace SampleWebAPI.Data.Repositories;

public class StudentRepository : IStudentRepository
{

    private readonly AppDbContext _dbContext;

    public StudentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Student> AddStudent(Student student)
    {
        _dbContext.Add(student);
        await _dbContext.SaveChangesAsync();
        return student;
    }

    public async Task<IEnumerable<Student>> GetAllStudents()
    {

        return await _dbContext.Students.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetMajorStudents()
    {
        return await _dbContext.Students.AsNoTracking().Where(s => s.Age >= 18).ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetMinorStudents()
    {
        return await _dbContext.Students.AsNoTracking().Where(s => s.Age < 18).ToListAsync();
    }

    public async Task<Student?> GetStudentById(int id)
    {
        // return await _dbContext.Students.FindAsync(id);
        /* return await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
         * the above method will return the first matching row 
         */

        // this will return if it has only one value.
        return await _dbContext.Students.SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task<int?> RemoveStudent(int id)
    {
        var student =  await _dbContext.Students.FindAsync(id);
        if (student != null)
        {
           _dbContext.Students.Remove(student);
           await _dbContext.SaveChangesAsync();
           return student.Id;
        }
        else
        {
            return null;
        }
        
    }
}

