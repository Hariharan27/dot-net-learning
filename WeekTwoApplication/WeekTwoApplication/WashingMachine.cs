using System;
namespace WeekTwoApplication
{
	public class WashingMachine:Appliance
	{
		public WashingMachine(string name) : base(name)
        {

        }

        public override void start()
        {
            Console.WriteLine("Washing Machine is started");
        }
    }
}

