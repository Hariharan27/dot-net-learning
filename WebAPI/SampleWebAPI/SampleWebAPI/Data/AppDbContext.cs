using System;
using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Models;

namespace SampleWebAPI.Data;

	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
		{
		}

		public DbSet<Person> Persons { get; set; }
		public DbSet<Student> Students { get; set; }

	}


