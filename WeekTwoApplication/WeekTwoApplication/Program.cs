using System;
using WeekTwoApplication;
using System.Globalization;



class Program
{
    public static void Main(string[] args)
    {
        Person p1 = new Person("Hariharan", 25);
        Person p2 = new Person("Vijaykumar");
        Console.WriteLine(p1.AgeValue);
        p1.Display();
        p2.Display();
        Employee e1 = new Employee("Advik", 2045);
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
        ExecptionExample();
        StringExample();
        CheckedUnChecked();

        // Generic Class
        GenericBox<int> genericBox = new GenericBox<int>();
        genericBox.setValue(32);
        Console.WriteLine($"the genericaValue is {genericBox.GetValue()}");

        int[] nums = { 1, 2, 3, 4, 5 };

        PrintArray(nums);

        MathOperation op;

        op = Program.Add;

        Console.WriteLine(op(5,3));

        CollectionExample collectionExample = new CollectionExample();
        collectionExample.DisplayListExample();
        collectionExample.DisplayHashSet();
        collectionExample.StackExample();
        collectionExample.QueueExample();
        collectionExample.SortedSetExample();

        collectionExample.ExampleLinkedList();
        collectionExample.ExampleOfDictionary();



        Console.ReadKey();

    }

    public static int Add(int a, int b) => a + b;

    delegate int MathOperation(int a, int b);


    static void CheckedUnChecked()
    {
        int a = int.MaxValue;
        int b = 1;
        try
        {
            int result = checked(a + b);

        }
        catch (OverflowException e)
        {
            Console.WriteLine($"The execption is {e.Message}");
        }

        Console.WriteLine($"The unchecked execption is {unchecked(a + b)}");
    }

    static void StringExample()
    {
        Console.WriteLine("Enter the first name:");
        String? fullname = Console.ReadLine();
        if (fullname != null)
        {
            fullname = fullname.Trim();
            fullname = fullname.ToLower();
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string titleCase = textInfo.ToTitleCase(fullname);
            Console.WriteLine($"Welcome, {titleCase} !");
        }
    }

    static void ExecptionExample()
    {
        try
        {
            Console.WriteLine("Enter the first number:");
            int num1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the second number:");
            int num2 = Convert.ToInt32(Console.ReadLine());

            int result = num1 / num2;
            Console.WriteLine($"The Result is:{result}");
        }
        catch (FormatException e)
        {
            Console.WriteLine($"Format Exception:{e.Message}");
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine($"The exception divide by zero message is {e.Message}");
        }
        catch (SystemException e)
        {
            Console.WriteLine($"The execption is {e.GetType().Name}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"The exception message is {e.Message}");
        }
        finally
        {
            Console.WriteLine("Thank you for using the program.");
        }
    }

    static void PrintArray<T>(T[] array)
    {

       foreach(T item in array)
        {
            Console.WriteLine(item);
        }

    }
}
