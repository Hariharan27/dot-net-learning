using System;
namespace WeekOneApplication
{
	public  class StaticExample
	{
        public static int a = 10;
        static StaticExample()
		{
		}

		public static void DisplayStatic()
		{
			Console.WriteLine($"{a}");
		}

	}
}

