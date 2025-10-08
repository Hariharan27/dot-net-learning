using System;
namespace WeekOneApplication
{
	public class Student
	{

		int age;
		double height;
		bool isStudent;
		char initial;
		string name;
		string hobby;
		dynamic dynamicValue;
		string? department;



        public Student(string name,int age,bool isStudent,char initial,string hobby,double height,dynamic dynamicValue,string? department)
		{
			this.name = name;
			this.age = age;
			this.isStudent = isStudent;
			this.initial = initial;
			this.height = height;
			this.hobby = hobby;
			this.dynamicValue = dynamicValue;
			this.department = department;
		}

		public void displayInformation()
		{
            var balance = 10; // inferred as int

            Console.WriteLine($"Name: {name}, Age: {age}, Height: {height}, initial: {initial}, Student:{isStudent}, Hobby:{hobby}, balance:{balance}");
		}
	}
}

