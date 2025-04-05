using System;
using System.Collections.Generic;
using System.Linq; // For case-insensitive search (FirstOrDefault)

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    // Optional: Override ToString for easier display
    public override string ToString()
    {
        return $"Name: {Name,-15} | Price: {Price,8:C} | Quantity: {Quantity,5}";
    }
}
public class Program
{
    // In-memory inventory list
    static List<Product> inventory = new List<Product>();
    const string CancelKeyword = "cancel"; // Define cancel keyword

    public static void Main(string[] args)
    {
        // Initialize with default items
        InitializeInventory();

        // Main application loop
        while (true) // Loop indefinitely until exited (or closed)
        {
            // Display inventory automatically at the start of each loop iteration
            DisplayInventory();

            // Prompt user for command
            Console.WriteLine("\nEnter command (Add, Update, Remove) or type 'Exit' to quit:");
            string input = Console.ReadLine()?.Trim(); // Read and trim input

            if (string.IsNullOrEmpty(input)) continue; // Skip empty input

            // Use switch for command handling (case-insensitive)
            switch (input.ToLower())
            {
                case "add":
                    AddProduct();
                    break;
                case "update":
                    UpdateProductStock();
                    break;
                case "remove":
                    RemoveProduct();
                    break;
                case "exit":
                    Console.WriteLine("Exiting application.");
                    return; // Exit the Main method, terminating the app
                default:
                    Console.WriteLine("Invalid command. Please use Add, Update, or Remove.");
                    break;
            }
            Console.WriteLine("----------------------------------------"); // Separator
        }
    }

    // Method to initialize default inventory
    static void InitializeInventory()
    {
        inventory.Add(new Product("Apple", 1.20m, 15));
        inventory.Add(new Product("Orange", 0.85m, 20));
        inventory.Add(new Product("Grape", 2.50m, 10)); // Example default values
    }

    // Method to display the current inventory
    static void DisplayInventory()
    {
        Console.WriteLine("\n--- Current Inventory ---");
        if (inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
        }
        else
        {
            // Using a for loop as requested (could also use foreach)
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine(inventory[i].ToString()); // Uses the overridden ToString
            }
        }
        Console.WriteLine("-------------------------");
    }

    // Method to handle adding a new product
    static void AddProduct()
    {
        Console.WriteLine("\n--- Add New Product ---");
        Console.WriteLine($"Type '{CancelKeyword}' at any prompt to cancel.");

        // Get Product Name (Validation: Not empty, Not duplicate)
        string name = GetValidatedStringInput("Enter product name: ");
        if (name == null) return; // Cancelled

        // Check for duplicates (case-insensitive)
        if (inventory.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"Error: Product '{name}' already exists. Add operation cancelled.");
            return;
        }

        // Get Product Price (Validation: Positive decimal)
        decimal? price = GetValidatedDecimalInput("Enter product price: ", mustBePositive: true);
        if (price == null) return; // Cancelled

        // Get Product Quantity (Validation: Non-negative integer)
        int? quantity = GetValidatedIntInput("Enter initial stock quantity: ", mustBeNonNegative: true);
        if (quantity == null) return; // Cancelled

        // Add the product
        inventory.Add(new Product(name, price.Value, quantity.Value));
        Console.WriteLine($"Product '{name}' added successfully.");
        // Inventory will be displayed automatically on the next loop iteration
    }

    // Method to handle updating stock quantity
    static void UpdateProductStock()
    {
        Console.WriteLine("\n--- Update Product Stock ---");
        Console.WriteLine($"Type '{CancelKeyword}' at any prompt to cancel.");

        // Get Product Name to Update
        string name = GetValidatedStringInput("Enter the name of the product to update: ");
        if (name == null) return; // Cancelled

        // Find the product (case-insensitive)
        Product productToUpdate = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        // Use if-else as requested
        if (productToUpdate == null)
        {
            Console.WriteLine($"Error: Product '{name}' not found. Update operation cancelled.");
        }
        else
        {
            Console.WriteLine($"Found product: {productToUpdate.Name}. Current quantity: {productToUpdate.Quantity}");

            // Get New Quantity (Validation: Non-negative integer)
            int? newQuantity = GetValidatedIntInput("Enter the new total stock quantity: ", mustBeNonNegative: true);
            if (newQuantity == null) return; // Cancelled

            // Update the quantity
            productToUpdate.Quantity = newQuantity.Value;
            Console.WriteLine($"Stock quantity for '{productToUpdate.Name}' updated successfully to {newQuantity.Value}.");
            // Inventory will be displayed automatically on the next loop iteration
        }
    }

    // Method to handle removing a product
    static void RemoveProduct()
    {
        Console.WriteLine("\n--- Remove Product ---");
        Console.WriteLine($"Type '{CancelKeyword}' at any prompt to cancel.");

        // Get Product Name to Remove
        string name = GetValidatedStringInput("Enter the name of the product to remove: ");
        if (name == null) return; // Cancelled

        // Find the product (case-insensitive)
        Product productToRemove = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (productToRemove == null)
        {
            Console.WriteLine($"Error: Product '{name}' not found. Remove operation cancelled.");
        }
        else
        {
            Console.WriteLine($"Found product: {productToRemove.ToString()}");
            // Confirmation required
            Console.Write("Are you sure you want to remove this product? (Y/N): ");
            string confirmation = Console.ReadLine()?.Trim().ToLower();

            // Check for cancellation during confirmation
            if (confirmation == CancelKeyword)
            {
                Console.WriteLine("Remove operation cancelled.");
                return;
            }

            // Use if-else for confirmation check
            if (confirmation == "y")
            {
                inventory.Remove(productToRemove);
                Console.WriteLine($"Product '{name}' removed successfully.");
                // Inventory will be displayed automatically on the next loop iteration
            }
            else
            {
                Console.WriteLine("Remove operation cancelled by user.");
            }
        }
    }

    // --- Helper Methods for Input Validation ---

    // Gets non-empty string input, checks for cancellation
    static string GetValidatedStringInput(string prompt)
    {
        string input;
        // Use while loop as requested
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine()?.Trim();

            if (string.Equals(input, CancelKeyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Operation cancelled.");
                return null; // Indicate cancellation
            }

            if (!string.IsNullOrEmpty(input))
            {
                return input; // Valid input
            }
            else
            {
                Console.WriteLine("Input cannot be empty. Please try again.");
            }
        }
    }

    // Gets validated decimal input, checks for cancellation
    static decimal? GetValidatedDecimalInput(string prompt, bool mustBePositive = false)
    {
        decimal value;
        // Use while loop as requested
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()?.Trim();

            if (string.Equals(input, CancelKeyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Operation cancelled.");
                return null; // Indicate cancellation
            }

            if (decimal.TryParse(input, out value))
            {
                // Use if-else as requested
                if (mustBePositive && value <= 0)
                {
                    Console.WriteLine("Invalid input. Price must be a positive number. Please try again.");
                }
                else
                {
                    return value; // Valid input
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
            }
        }
    }

    // Gets validated integer input, checks for cancellation
    static int? GetValidatedIntInput(string prompt, bool mustBeNonNegative = false)
    {
        int value;
        // Use while loop as requested
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()?.Trim();

            if (string.Equals(input, CancelKeyword, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Operation cancelled.");
                return null; // Indicate cancellation
            }

            if (int.TryParse(input, out value))
            {
                // Use if-else as requested
                if (mustBeNonNegative && value < 0)
                {
                    Console.WriteLine("Invalid input. Quantity cannot be negative. Please try again.");
                }
                else
                {
                    return value; // Valid input
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid whole number.");
            }
        }
    }
}