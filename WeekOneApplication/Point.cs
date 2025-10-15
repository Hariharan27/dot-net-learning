using System;
namespace WeekOneApplication
{
	public struct Point
	{
		int X, Y;

		public Point(int x,int y) {
			X = x;
			Y = y;
		}

		public void Display()
		{
			Console.WriteLine($"Point({X},{Y})");
		}
	}
}

