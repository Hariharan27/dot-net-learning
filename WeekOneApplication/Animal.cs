using System;
namespace WeekOneApplication
{
	public class Animal
	{
        private string name;

        public Animal(string name)
		{
			this.name = name;
			Console.WriteLine($"name of the animal is {name}");
		}

		public void Eat()
		{
			Console.WriteLine("Animal is Eating");
		}

		public virtual void MakesSound()
		{
			Console.WriteLine("Animal makes sound");
		}

	}

	public class Mammal:Animal {

		public Mammal(string name):base(name)
		{
			
		}


		public void Walk()
		{
			Console.WriteLine("Mammal is Waling");
		}
	}


	public class Dog:Mammal
	{

		public Dog(string name):base(name)
		{
			
		}

        public override void MakesSound()
        {
            base.MakesSound();
			Console.WriteLine("Dog barks");
        }

        public new void Eat()
		{
			base.Eat();
			Console.WriteLine("Dog eat bones");
		}

		public void Bark()
		{
			Console.WriteLine("Dog is Barking");
		}

	}

	public class Cat:Mammal
	{
		public Cat(string name):base(name)
		{

		}
		public void Meow()
		{
			Console.WriteLine("Cat is Meow");
		}
	}
}

