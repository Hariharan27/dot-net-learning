using System;
namespace WeekOneApplication
{
	public class Library
	{
		public String Name { set; get; }

		public List<Book> Books { set; get; }

		public Library(string name, List<Book> books)
		{
			Name = name;
			Books = books;
		}

		public void DisplayBooks()
		{
			Console.WriteLine($"The {Name} Libaray Has the Following Books: ");
			foreach (Book book in Books)
			{
                Console.WriteLine(book.Name);

            }
        }


		public void AddBook(Book book)
		{
			Books.Add(book);
		}

		public void RemoveBook(Book book)
		{
			Books.Remove(book);
		}

    }
}

