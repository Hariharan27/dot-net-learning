using System;
namespace WeekTwoApplication
{
	public class CollectionExample
	{
        private List<String> strings = new List<string>();

        public CollectionExample()
		{
            strings.Add("Hariharan");
			strings.Add("Dot-Net");
            strings.Add("Vijay");
			strings.Add("Peace");
			strings.Remove("Peace");
			strings.RemoveAt(1);
        }

		public void DisplayListExample()
		{
			Console.WriteLine(strings.Contains("Vijay"));
			Console.WriteLine(strings.IndexOf("Hariharan"));
			Console.WriteLine(strings.Find(s => s.Length > 3));
			foreach(string name in strings)
			{
				if (name.StartsWith("Hari"))
				{
					Console.WriteLine("This is my name");
				}
				Console.WriteLine(name);
			}
			
		}

		public void DisplayHashSet()
		{
			HashSet<int> hashset = new HashSet<int>();
			hashset.Add(10);
			hashset.Add(20);
			hashset.Add(30);
			hashset.Add(40);
			hashset.Add(100);

			hashset.Remove(100);

			hashset.RemoveWhere(n => n > 30);

			hashset.Contains(20);

            HashSet<int> otherSet = new HashSet<int> { 20, 40, 50 };

			hashset.UnionWith(otherSet);

			hashset.IntersectWith(otherSet);

			foreach (int i in hashset)
			{
				Console.Write(i);
			}
        }

		public void SortedSetExample()
		{
			SortedSet<int> sortedSetExample = new SortedSet<int>();
			sortedSetExample.Add(10);
			sortedSetExample.Add(50);
			sortedSetExample.Add(30);
			sortedSetExample.Add(20);
			sortedSetExample.Add(40);

			foreach(int i in sortedSetExample)
			{
				Console.WriteLine(i);
			}
		}


		public void StackExample()
		{
			Stack<int> numbers = new Stack<int>();
			numbers.Push(10);
			numbers.Push(20);
			numbers.Push(30);

			Console.WriteLine(numbers.Peek());

			int lastElement = numbers.Pop();

			Console.WriteLine($"====>{lastElement}");

			foreach(int i in numbers)
			{
				Console.WriteLine(i);
			}

		}

		public void QueueExample()
		{
			Queue<int> numbers = new Queue<int>();
			numbers.Enqueue(10);
			numbers.Enqueue(20);
			numbers.Enqueue(30);
			numbers.Enqueue(40);
			Console.WriteLine(numbers.Peek());
			int firstElement = numbers.Dequeue();

			foreach(int i in numbers)
			{
				Console.WriteLine(i);
			}
		}

		public void ExampleLinkedList()
		{
			LinkedList<int> numbers = new LinkedList<int>();
			numbers.AddFirst(10);
			numbers.AddLast(20);
			Console.WriteLine(String.Join(",",numbers));
			numbers.AddLast(30);
            Console.WriteLine(String.Join(",", numbers));

			if(numbers.First != null)
			Console.WriteLine(numbers.First.Value);
			if(numbers.Last != null)
			Console.WriteLine(numbers.Last.Value);

			var node = numbers.Find(20);
			if(node != null)
			{
                numbers.AddAfter(node, 25);
                numbers.AddBefore(node, 15);
            }

            Console.WriteLine(String.Join(",", numbers));


        }

		public void ExampleOfDictionary()
		{
            SortedDictionary<int, string> students = new SortedDictionary<int, string>();
			students[1] = "Hariharan";
			students[2] = "Vijay";
			students.Add(3,"Ramesh");
			students.Add(4, "Suresh");
			students.Remove(4);
			students.ContainsKey(4);
			students.ContainsValue("Hariharan");

			string? valueName;
			if(students.TryGetValue(3, out valueName))
			{
                Console.WriteLine(valueName);

            }


            foreach (KeyValuePair<int, string> keyValuePair in students)
			{
				Console.WriteLine($"Key:{keyValuePair.Key} and Value:{keyValuePair.Value}");
			}


        }

		public void ExampleOfSortedList()
		{
			SortedList<int, string> students = new SortedList<int, string>();
			students.Add(101, "Hariharan");
			students.Add(102, "Vijay");
			students.Add(103, "Ramesh");

			foreach(var kvp in students)
			{
				Console.WriteLine($"Key:{kvp.Key}, Value:{kvp.Value}");
			}

			Console.WriteLine($"First Element in the sortedList:{students.GetValueAtIndex(0)}");

		}


    }
}

