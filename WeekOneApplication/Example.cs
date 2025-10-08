using System;
namespace WeekOneApplication
{
	public class Example
	{
		int age;
		String name;

		public Example(String name,int age){
			this.name = name;
			this.age = age;
		}

		public void ShowMyDetails()
		{
			Console.WriteLine($"Hello, My Name is {name} and Age is {age}");

		}
	}
}

