using System;
using WeekTwoApplication;

 class Program
    {
        public static void Main(string[] args)
        {
            Person p1 = new Person("Hariharan",25);
            Person p2 = new Person("Vijaykumar");
            Console.WriteLine(p1.AgeValue);
            p1.Display();
            p2.Display();
            Employee e1 = new Employee("Advik",2045);
            e1.Display();

            SportsCar sportsCar = new SportsCar();
            // sportsCar.Drive();

            WashingMachine machine = new WashingMachine("LG");
            machine.showBrand();
            machine.start();

            Refrigerator refrigerator = new Refrigerator("Samsung");
            refrigerator.showBrand();
            refrigerator.start();

            CreditCard card = new CreditCard();
            card.Pay();
            card.Refund();

            UPI uPI = new UPI();
            uPI.Pay();
            uPI.Refund();

            Console.ReadKey();

        }
    }
