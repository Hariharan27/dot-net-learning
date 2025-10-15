using System;
namespace WeekTwoApplication
{
	public class Person
	{

        private string SSN = "123-45-6789";       // Only in class
        public string NameValue = "Hari";              // Anywhere
        protected string Email = "hari@mail.com"; // Derived classes
        internal int AgeValue = 25;                    // Same assembly
        protected internal string City = "Chennai";
        private protected string Country = "India";

        public int Age { set; get; }

		public string Name { set; get; }

		public Person(String name)
		{
			Name = name;
		}

		public Person(String name, int age)
		{
			Name = name;
			Age = age;
		}

		public virtual void Display()
		{
			Console.WriteLine($"The person name is {Name} and Age is {Age}");
		}


	}
}

