using System;
namespace WeekOneApplication
{
	public class ConditionalStatement
	{
		int? age;
        int? day;

        /***
         * Write a C# program that prints numbers from 1 to 20, but with the following rules:
         * Skip all numbers that are multiples of 3
         * Stop printing completely if the number reaches 15
         ***/
        public void ExampleOfBreakContinue()
        {
            for (int i=1; i <= 20; i++)
            {
                if(i > 15)
                {
                    break;
                }else if(i % 3 == 0)
                {
                    continue;
                }else
                {
                    Console.Write($"{i} ");
                }
            }
            Console.WriteLine("");
        }

        public void GetDayInputFromUser()
        {
            Console.WriteLine("Please enter the day number (1-7)");
            string? tempDay = Console.ReadLine();
            if (tempDay != null) {
                day = int.Parse(tempDay);
            }
        }

        /**
         * Write a C# program that asks the user to enter a password.
         * Keep asking until the user enters the correct password "letmein".
         * Use a do-while loop to ensure the prompt appears at least once.
         **/

        public void ValidateUserPassword()
        {
            string? password;
            do
            {
                Console.WriteLine("Please enter a user password");
                password = Console.ReadLine();
            } while (password != "letmein");
        }


        public void PrintMultiplicationTable()
        {
            Console.WriteLine("Enter the number");
            int userInput = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Multiplication of table: {userInput}");
            for (int i=1;i<=10;i++)
            {
                Console.WriteLine($"{userInput} * {i} = {(i*userInput)}");
            }
        }

        public void ShowEvenNumberUptoN(int n)
        {
            int i = 1;
            while(i <= n)
            {
                if(i%2 == 0)
                {
                    Console.Write($"{i} ");
                }
                i++;
            }
            Console.WriteLine("");

        }

        public void PrintTheDayDetails() {
            if(day != null)
            {
                switch(day)
                {
                    case 1:
                        Console.WriteLine("Monday");
                        break;
                    case 2:
                        Console.WriteLine("Tuesday");
                        break;
                    case 3:
                        Console.WriteLine("Wednesday");
                        break;
                    case 4:
                        Console.WriteLine("Thursday");
                        break;
                    case 5:
                        Console.WriteLine("Friday");
                        break;
                    case 6:
                        Console.WriteLine("Saturday");
                        break;
                    case 7:
                        Console.WriteLine("Sunday");
                        break;
                    default:
                        Console.WriteLine("Please enter a valid day index number");
                        break;
                }
            }
        }

		public void GetUserAge()
		{
			Console.WriteLine("Please enter the age:");
			var tempAge = Console.ReadLine();
			if(tempAge != null)
			{
                age = int.Parse(tempAge);
            }

        }

		public void ShowUserAgeClassification()
		{
            if (age != null) {

                if (age < 13) {
                    Console.WriteLine("You are a Child");
                }
                else if (age >= 13 && age < 20 ){
                    Console.WriteLine("You are Teenager");
                }
                else {
                    Console.WriteLine("You are a Adult");
                }
            }
        }
         
	}
}

