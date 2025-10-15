using System;
namespace WeekOneApplication
{

    /**
	 * Create a class Car that represents a car with the following:
     * Properties: Brand, Model, and Year
     * Method: DisplayDetails() that prints all car details
     *  Then, in the Main() method, create two car objects and display their details.
     *  Car 1 Details: Toyota Corolla 2020
Car  *  Car 2 Details: Honda City 2022
     **/
    public class Car
	{
		public string Brand { set; get;}
		public string Model { set; get; }
		public long Year { set; get; }

		public Car(string brand, string model,long year)
		{
			this.Brand = brand;
			this.Model = model;
			this.Year = year;
		}

		public void DisplayDetails()
		{
			Console.WriteLine($"The Car Brand is {Brand} and the model is {Model} and the year of manufacturing is {Year}");
		}

	}
}

