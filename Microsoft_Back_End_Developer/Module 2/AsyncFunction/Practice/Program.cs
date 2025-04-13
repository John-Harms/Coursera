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
    public static void BookLookup()
    {
        Console.WriteLine("Type in something to search!");
        string searchText = Console.ReadLine();
        foreach (Book book in bookList)
        {
            if (book.Name.ToLower().Contains(searchText.ToLower()))
            {
                Console.WriteLine(book.Name);
            }

        }

        bool found = false;
        while (!found)
        {
            Console.WriteLine("Which specific book do you want?");
            string checkoutBook = Console.ReadLine();
            foreach (Book book in bookList)
            {
                if (book.Name.ToLower() == checkoutBook.ToLower())
                {
                    if (book.CheckedOut == true)
                    {
                        Console.WriteLine("This book is checked out, please select another!");
                        found = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You have checked out the book!");
                        book.CheckedOut = true;
                        userCheckouts.Add(book.Name);
                        found = true;
                        break;
                    }
                }
            }
            if (!found)
            {
                Console.WriteLine("No Book found you dingus.");
                Console.WriteLine("Want to lookup another? Yes/No");
                string decision = Console.ReadLine();
                if (decision.ToLower() == "yes")
                { }
                else if (decision.ToLower() == "no")
                {
                    Console.WriteLine("Ok. Exiting Selection");
                    break;
                }
            }

        }
    }

    public static void ReviewList()
    {
        Console.WriteLine("Here are your books!");
        foreach (string book in userCheckouts)
        {
            Console.WriteLine($"{book}");
        }
        Console.WriteLine("Type in the name of the book you want to return!");
        

    }
    public static Book[] bookList = new Book[]
        {
        new Book("The Dumb Coder", false),
        new Book("My Roomates are Bums", false),
        new Book("My Roomates are Bums Part 2", false)
        };

    public static List<string> userCheckouts = new List<string>();
    public static void Main(string[] args)
    {
        bool runningProgram = true;
        while (runningProgram)
        {
            Console.WriteLine("Welcome! Type 1 to lookup a book. Type 2 to review your checkouts.");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                BookLookup();
            }
            else if (choice == "2")
            {
                ReviewList();
            }
        }
    }
}