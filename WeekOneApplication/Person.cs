using System;
namespace WeekOneApplication
{
	public class Person
	{
		private string name;

		private const string Species = "Human";

		private readonly int birthYear;

		static int totalObjects = 0;

        public Person(string name,int birthYear) {
			this.name = name;
			totalObjects += 1;
			this.birthYear = birthYear;
		}

		public void DisplayDetails()
		{
			string message = $"Name:{name} Total Persons Created:{totalObjects} Species:{Species} Birth Year:{birthYear}";
            Console.WriteLine(message);
		}

    }
}

