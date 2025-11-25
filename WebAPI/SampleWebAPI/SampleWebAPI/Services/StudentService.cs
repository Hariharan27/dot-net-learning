using System;
using SampleWebAPI.Models;
using SampleWebAPI.Data.Repositories;

namespace SampleWebAPI.Services
{
	public class StudentService: IStudentService
	{
        private readonly IStudentRepository _studentRepository;

		public StudentService(IStudentRepository studentRepository)
		{
            _studentRepository = studentRepository;
		}

        public async Task<Student> AddStudent(Student student)
        {
            return await _studentRepository.AddStudent(student);
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _studentRepository.GetAllStudents();
        }

        public async Task<IEnumerable<Student>> GetMajorStudents()
        {
            return await _studentRepository.GetMajorStudents();
        }

        public async Task<IEnumerable<Student>> GetMinorStudents()
        {
            return await _studentRepository.GetMinorStudents();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _studentRepository.GetStudentById(id);
        }

        public async Task<int?> RemoveStudent(int id)
        {
            return await _studentRepository.RemoveStudent(id);
        }
    }
}

