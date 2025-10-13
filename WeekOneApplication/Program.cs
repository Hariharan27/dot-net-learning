using System;
using WeekOneApplication;

class Program
{
    static void Main()
    {
        // Sample progrom for the topic example 
        Example example = new Example("Hariharan", 30);
        example.ShowMyDetails();
        /*
         * Sample program to conver the following topics 
         * local variable 
         * instance variable
         * static variable
         */
        Person hari = new Person("Hariharan", 1994);
        hari.DisplayDetails();
        Person sri = new Person("Srikanth", 1995);
        sri.DisplayDetails();
        Person vimal = new Person("Vimal", 1995);
        vimal.DisplayDetails();

        /* data type with value type and reference type
         * var infered type only in local scope 
         * dynamic 
         * nullable type 
         */
        Student student = new Student("Hariharan", 30, false, 'S', "Internet Surfing", 164.5, "Hello", null);
        student.displayInformation();
        
        // if example 
        ConditionalStatement conditionalStatement = new ConditionalStatement();
        conditionalStatement.GetUserAge();
        conditionalStatement.ShowUserAgeClassification();
        /*
         * Switch example 
         */
        conditionalStatement.GetDayInputFromUser();
        conditionalStatement.PrintTheDayDetails();
        // for loop example 
        conditionalStatement.PrintMultiplicationTable();
        // while loop example
        conditionalStatement.ShowEvenNumberUptoN(20);
        // do while loop example
        conditionalStatement.ValidateUserPassword();

        // example of continue and break
        conditionalStatement.ExampleOfBreakContinue();


        FunctionExample functionExample = new FunctionExample();
        int num = 10;
        functionExample.CallByValueExampleMethod(num);
        Console.WriteLine($"num call by value in the {num}");
        functionExample.CallByReferenceMethod(ref num);
        Console.WriteLine($"num call by reference in the {num}");
        int value;
        functionExample.ExampleOutKeyword(out value);
        Console.WriteLine($"The output is from the value {value}");



        ArrayExample arrayExample = new ArrayExample();
        arrayExample.initializeArray();
        arrayExample.arrayOperations();

        int[] numberArray = { 10, 20, 30, 40, 50 };
        Console.WriteLine(string.Join(",", numberArray));


        arrayExample.ChangeTheArray(numberArray);
        arrayExample.TwoDimentionalArray();
        arrayExample.ThreeDimentionalArray();
        arrayExample.ExampleOfJaggedArray();

        Console.ReadKey();
    }
}

