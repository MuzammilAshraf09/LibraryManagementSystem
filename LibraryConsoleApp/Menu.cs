//showing Menu here
using System;

namespace LibraryConsoleApp
{
    public static class Menu
    {
        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("***Library Management System***");
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. Remove a book");
            Console.WriteLine("3. Update a book");
            Console.WriteLine("4. Register a new borrower");
            Console.WriteLine("5. Update a borrower");
            Console.WriteLine("6. Delete a borrower");
            Console.WriteLine("7. Borrow a book");
            Console.WriteLine("8. Return a book");
            Console.WriteLine("9. Search for books by title, author, or genre");
            Console.WriteLine("10. View all books");
            Console.WriteLine("11. View borrowed books by a specific borrower");
            Console.WriteLine("12. Exit the application");
            Console.Write("Select an option: ");
        }

        public static string GetUserInput()
        {
            return Console.ReadLine();
        }
 

        public static void WaitForUser()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
