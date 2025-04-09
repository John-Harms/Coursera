using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public class Book
    {
        public string Name { get; set; }
        public bool CheckedOut { get; set; }

        public Book(string name, bool checkedout)
        {
            Name = name;
            CheckedOut = checkedout;
        }
    }
    public static Book[] bookList = new Book[]
        {
        new Book("The Dumb Coder", false),
        new Book("My Roomates are Bums", false),
        new Book("My Roomates are Bums Part 2", false)
        }; public static void Main(string[] args)
    {
        Console.WriteLine("Welcome! Type 1 to lookup a book. Type 2 to review your checkouts.");
        string choice = Console.ReadLine();
        if (choice == "1")
        {
            Console.WriteLine("Type in something to search!");
            string searchText = Console.ReadLine();
            foreach (Book book in bookList)
            {
                if (book.Name.Contains(searchText))
                {
                    Console.WriteLine(book.Name);
                }

            } 
            Console.WriteLine("Which specific book do you want?");
            string checkoutBook = Console.ReadLine();
            if ()
            {
                Console.WriteLine("Sorry this book is checked out.");
            }
            else
            {
                Console.WriteLine("Want to Checkout? Type Yes/No");
                string checkOutDecision = Console.ReadLine();
                if (checkOutDecision.ToLower() == "yes")
                    foreach (Book book in bookList) {
                        if (book.Name == )
                    }
                {

                }
            }
        }
        else if (choice == "2")
        {

            Console.WriteLine($"You have checked out");
        }
    }
}