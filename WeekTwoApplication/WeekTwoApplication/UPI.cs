using System;
namespace WeekTwoApplication
{
	public class UPI:IPayment
	{
		public UPI()
		{
		}

        public void Pay()
        {
            Console.WriteLine("UPI Payment Done");
        }

        public void Refund()
        {
            Console.WriteLine("UPI Refund is in progress");
        }
    }
}

