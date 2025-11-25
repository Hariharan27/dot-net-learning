using System;
using SampleWebAPI.Models;
namespace SampleWebAPI.Services
{
	public interface IStudentService
	{
		Task<IEnumerable<Student>> GetAllStudents();
		Task<Student?> GetStudentById(int id);
		Task<Student> AddStudent(Student student);
		Task<int?> RemoveStudent(int id);
		Task<IEnumerable<Student>> GetMajorStudents();
		Task<IEnumerable<Student>> GetMinorStudents();
	}

}

