using System;
namespace WeekTwoApplication
{
	public class Refrigerator:Appliance
	{
		public Refrigerator(string name):base(name)
		{
		}

        public override void start()
        {
            Console.WriteLine("Refrigerator is started cooling");
        }
    }
}

