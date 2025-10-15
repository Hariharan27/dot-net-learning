using System;
namespace WeekTwoApplication
{
	public class Employee: Person
	{
		int joinedYear;

		public Employee(string name,int joinedYear):base(name)
		{
			this.joinedYear = joinedYear;
		}

        public override void Display()
        {
			Console.WriteLine($"The Employee name is {Name} and Joined Year is {joinedYear}");
        }

    }
}

