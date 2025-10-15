using System;
namespace WeekTwoApplication
{
	public class Car:Vehicle
	{
		public Car()
		{
		}

        public sealed override void Drive()
        {
            Console.WriteLine("Car is driving.");
        }

    }
}

