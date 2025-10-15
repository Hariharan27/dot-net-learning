using System;
namespace WeekTwoApplication
{
	public class CreditCard:IPayment
	{
		public CreditCard()
		{
		}

        public void Pay()
        {
            Console.WriteLine("Credit Card Payment Done.");
        }

        public void Refund()
        {
            Console.WriteLine("Credit Card Refund is inprogrss");
        }
    }
}

