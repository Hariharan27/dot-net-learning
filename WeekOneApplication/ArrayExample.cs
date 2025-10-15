using System;
namespace WeekOneApplication
{
	public class ArrayExample
	{
		public void initializeArray()
		{
			int[] arrayOne = new int[5];
			arrayOne[0] = 1;
			arrayOne[1] = 2;
			Console.WriteLine(string.Join(", ", arrayOne));

			int[] arrayTwo = { 1, 2, 3, 4, 5, 6 };
			Console.WriteLine(string.Join(",", arrayTwo));
		}

		public void arrayOperations() {
			int[] arrayList = { 10, 20, 30, 40, 50 };
			for (int i = 0; i < arrayList.Length; i++)
			{
				Console.Write($"{arrayList[i]}, ");
			}

			foreach(int i in arrayList)
			{
				Console.Write($"\n{i}");
			}

			Array.Sort(arrayList);
			Array.IndexOf(arrayList,30);
			Array.Clear(arrayList, 0, arrayList.Length);
		}

		public void ChangeTheArray(int [] array)
		{

			for (int i = 0; i< array.Length;i++)
			{
				array[i] = array[i] * 2;
			}
		
		}


		public void TwoDimentionalArray()
		{
			int[,] twoArray = new int[2, 3]
			{
				{1,2,3},
				{4,5,6}
			};

			for (int i=0; i< twoArray.GetLength(0); i++)
			{
				for (int j = 0; j < twoArray.GetLength(1); j++)
				{
					Console.WriteLine($"The row number is {i} and the coulmn number is {j} and the value is {twoArray[i,j]}");
				}
			}

		}

		public void ThreeDimentionalArray()
		{
			int[,,] threeArray = new int[2, 3, 3]
			{
				{{1,2,3},{4,5,6},{ 7,8,9} },
                {{1,2,3},{4,5,6},{ 7,8,9} }
            };

			for (int i =0; i< threeArray.GetLength(0);i++)
			{
				for (int j = 0; j < threeArray.GetLength(1); j++)
				{
					for (int k=0; k < threeArray.GetLength(2); k++)
					{
						Console.WriteLine($"{threeArray[i,j,k]}");
					}
				}
			}

		}


		public void ExampleOfJaggedArray()
		{
			int[][] jaggerArray = new int[2][];

			jaggerArray[0] = new int[] { 1, 2, 3 };
			jaggerArray[1] = new int[] { 2, 6 };


			for(int i=0; i< jaggerArray.Length; i++)
			{
				for (int j=0; j < jaggerArray[i].Length; j++)
				{
					Console.WriteLine($"The Element is {jaggerArray[i][j]}");
				}
			}

		}


		public void ExampleofParams(string title, string[] names)
		{
			Console.WriteLine($"{title}: {string.Join(",",names)}");
		}

	}
}

