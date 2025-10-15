using System;
namespace WeekOneApplication
{
	public class Bird: IFly,IRun
	{
		public Bird()
		{
		}

        public void fly()
        {
			Console.WriteLine("Bird can fly");
        }

        public void run()
        {
			Console.WriteLine("Bird can run");
        }
    }

	interface IFly
	{
		void fly();

	}

	interface IRun
	{
        void run();
    }
}

