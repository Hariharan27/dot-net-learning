using System;
namespace WeekOneApplication
{
	public class FunctionExample
	{

	    public void CallByValueExampleMethod(int num)
		{
			num = num + 5;
			Console.WriteLine($"The number inside call by value {num}");
		}

		public void CallByReferenceMethod(ref int num)
		{
			num = num + 5;
			Console.WriteLine($"The number inside call by reference {num}");
		}

		public void ExampleOutKeyword(out int num)
		{
			num = 5 * 5;
		}

	}
}

