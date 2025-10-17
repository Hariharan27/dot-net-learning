using System;
namespace WeekTwoApplication
{

    // for value type its struct
    // for reference type its class
    // where T : struct → value type
	// where T : new () → must have a parameterless constructor
	// where T : BaseClass → must inherit a class
	// where T : InterfaceName → must implement an interface


    public class GenericBox<T> where T: struct 
	{

		private T? value;

		public void setValue(T value)
		{
			this.value = value;
		}

		public T? GetValue()
		{
			return value;
		}
	}


	interface IRepository<T>
	{
		void setValue(T value);
		T GetValue();

	}


}

