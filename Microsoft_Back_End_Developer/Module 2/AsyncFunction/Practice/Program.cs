using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public class LoopWrapper
    {
        public bool IsRunning { get; set; }

        public LoopWrapper()
        {
            IsRunning = true;
        }

        public void RunLoop(Action<LoopWrapper> loopAction)
        {
            while (IsRunning)
            {
                try
                {
                    loopAction(this);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during loop execution: {ex.Message}");
                }
            }
        }
    }
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
        LoopWrapper myLoop = new LoopWrapper();
        Action<LoopWrapper> myAction = (wrapper) =>
        {
            if (userCheckouts.Count() < 3)
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
                            wrapper.IsRunning = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You have checked out the book!");
                            book.CheckedOut = true;
                            userCheckouts.Add(book.Name);
                            wrapper.IsRunning = false;
                            break;
                        }
                    }
                }
            } else {
                Console.WriteLine("You can only reserve 3 books maximum.");
                wrapper.IsRunning = false;
            }
            if (wrapper.IsRunning)
            {
                Console.WriteLine("No Book found you dingus.");
                Console.WriteLine("Want to lookup another? Yes/No");
                string decision = Console.ReadLine();
                if (decision.ToLower() == "yes")
                { }
                else if (decision.ToLower() == "no")
                {
                    Console.WriteLine("Ok. Exiting Selection");
                    wrapper.IsRunning = false;
                }
            }
        };
        myLoop.RunLoop(myAction);
    }

    public static void ReviewList()
    {
        Console.WriteLine($"You have checked out {userCheckouts.Count()} books.");
        if (userCheckouts.Count() > 0)
        {
            foreach (string book in userCheckouts)
            {
                Console.WriteLine($"{book}");
            }
            Console.WriteLine("Type in the name of the book you want to return!");
            string decision = Console.ReadLine();
            foreach (Book book in bookList)
            {
                if (book.Name.ToLower() == decision.ToLower())
                {
                    if (book.CheckedOut == true && userCheckouts.Contains(book.Name))
                    {
                        Console.WriteLine("Book Successfully returned!");
                        userCheckouts.Remove(book.Name);
                        book.CheckedOut = false;
                        return;
                    }
                }
            }
            Console.WriteLine("Cant Return That!");

        }
    }
    public static Book[] bookList = new Book[]
        {
        new Book("The Dumb Coder", false),
        new Book("My Roomates are Bums", false),
        new Book("My Roomates are Bums Part 2", false),
        new Book("The Mysterious Fourth Book", false)
        };

    public static List<string> userCheckouts = new List<string>();
    public static void Main(string[] args)
    {
        LoopWrapper mainLoop = new LoopWrapper();
        Action<LoopWrapper> myAction = (wrapper) =>
        {
            Console.WriteLine("Welcome! Type 1 to lookup a book. Type 2 to review your checkouts. 3 to exit program.");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                BookLookup();
            }
            else if (choice == "2")
            {
                ReviewList();
            }
            else if (choice == "3")
            {
                Console.WriteLine("Program Exited!");
                wrapper.IsRunning = false;
            }
        };
        mainLoop.RunLoop(myAction);
    }
}