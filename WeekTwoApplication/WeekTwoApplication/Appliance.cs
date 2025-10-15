using System;
namespace WeekTwoApplication
{
	public abstract class Appliance
	{
		public string BrandName { set; get;}

		public Appliance(string brandname)
		{
			BrandName = brandname;
		}

		public abstract void start();

		public void showBrand()
		{
			Console.WriteLine($"Brand Name:{BrandName}");
		}


	}
}

