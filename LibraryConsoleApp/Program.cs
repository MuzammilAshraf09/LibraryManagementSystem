using System;

using LibraryDAL;
namespace LibraryConsoleApp
{
    class Program
    {
        static DataAccess dataAccess = new DataAccess();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Menu.DisplayMenu();  // menuClass to display menu 

                switch (Menu.GetUserInput())
                {
                    case "1":
                        AddNewBook();
                        break;
                    case "2":
                        RemoveTheBook();
                        break;
                    case "3":
                        UpdateBook();
                        break;
                    case "4":
                        RegisterNewBorrower();
                        break;
                    case "5":
                        UpdateBorrower();
                        break;
                    case "6":
                        DeleteBorrower();
                        break;
                    case "7":
                        BorrowBook();
                        break;
                    case "8":
                        ReturnBook();
                        break;
                    case "9":
                        SearchBooks();
                        break;
                    case "10":
                        ViewAllBooks();
                        break;
                    case "11":
                        ViewBorrowedBooksByBorrowerId();
                        break;
                    case "12":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }

                if (!exit)
                {
                    Menu.WaitForUser();
                }
            }
        }

        static void AddNewBook()   // add a new bok 
        {
            Console.WriteLine("Add a New Book");

            int bookId;
            Console.Write("Enter Book ID: ");
            try
            {
                bookId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)  // if input not valid 
            {
                Console.WriteLine("Invalid input format for Book ID. Please enter a numeric value ");
                return;
            }

            if (!ErrorHandles.ValidateBookId(bookId))
                return;

            Console.Write("Enter Title: ");
            string title = Menu.GetUserInput();

            Console.Write("Enter Author: ");
            string author = Menu.GetUserInput();

            Console.Write("Enter Genre: ");
            string genre = Menu.GetUserInput();

            Console.Write("Is the book available (true/false): ");
            bool isAvailable;
            try
            {
                isAvailable = bool.Parse(Menu.GetUserInput());
            }
            catch (FormatException)  //input validation
            {
                Console.WriteLine("Invalid input format for availability. Please enter 'true' or 'false' ");
                return;
            }

            Book book = new Book { BookId = bookId, Title = title, Author = author, Genre = genre, IsAvailable = isAvailable };
            dataAccess.AddBook(book);

            Console.WriteLine("Book added successfully ");
        }

        static void RemoveTheBook() // removing a book 
        {
            Console.WriteLine("Remove a Book");

            int bookId;
            Console.Write("Enter Book ID: ");
            try
            {
                bookId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException) // input is invlaid then exception throw
            {
                Console.WriteLine("Invalid input format for Book ID. Please enter a numeric value ");
                return;
            }

            dataAccess.RemoveBook(bookId);

        }

        static void UpdateBook() //update a book
        {
            Console.WriteLine("Update a Book");

            int bookId;
            Console.Write("Enter Book ID: ");
            try
            {
                bookId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Book ID. Please enter a numeric value ");
                return;
            }

            Book existingBook = dataAccess.GetBookById(bookId); // chk that boook is exist or not already 
            if (existingBook == null)
            {
                Console.WriteLine("Book not found ");
                return;
            }

            Console.Write("Enter Title: ");
            string title = Menu.GetUserInput();

            Console.Write("Enter Author: ");
            string author = Menu.GetUserInput();

            Console.Write("Enter Genre: ");
            string genre = Menu.GetUserInput();

            Console.Write("Is the book available (true/false): ");
            bool isAvailable;
            try
            {
                isAvailable = bool.Parse(Menu.GetUserInput());
            }
            catch (FormatException) // valid input chk 
            {
                Console.WriteLine("Invalid input format for availability. Please enter 'true' or 'false' ");
                return;
            }

            Book book = new Book { BookId = bookId, Title = title, Author = author, Genre = genre, IsAvailable = isAvailable };
            dataAccess.UpdateBook(book);

        }

        static void RegisterNewBorrower() // register a new borrower
        {
            Console.WriteLine("Register a New Borrower");

            int borrowerId;
            Console.Write("Enter Borrower ID: ");
            try
            {
                borrowerId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Borrower ID. Please enter a numeric value.");
                return;
            }

            if (!ErrorHandles.ValidateBorrowerId(borrowerId)) // validate the unique IDs
                return;

            Console.Write("Enter Name: ");
            string name = Menu.GetUserInput();

            Console.Write("Enter Email: ");
            string email = Menu.GetUserInput();
            if (!ErrorHandles.ValidateEmailFormat(email)) // validation for email 
                return;

            Borrower borrower = new Borrower { BorrowerId = borrowerId, Name = name, Email = email };
            dataAccess.RegisterBorrower(borrower);

            Console.WriteLine("Borrower registered successfully ");
        }

        static void UpdateBorrower() // updatte a borrower
        {
            Console.WriteLine("Update a Borrower");

            int borrowerId;
            Console.Write("Enter Borrower ID: ");
            try
            {
                borrowerId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Borrower ID. Please enter a numeric value.");
                return;
            }

            Console.Write("Enter Name: ");
            string name = Menu.GetUserInput();

            Console.Write("Enter Email: ");
            string email = Menu.GetUserInput();
            if (!ErrorHandles.ValidateEmailFormat(email))
                return;

            Borrower borrower = new Borrower { BorrowerId = borrowerId, Name = name, Email = email };
            dataAccess.UpdateBorrower(borrower);

        }

        static void DeleteBorrower() // deleting a borrower
        {
            Console.WriteLine("Delete a Borrower");

            int borrowerId;
            Console.Write("Enter Borrower ID: ");
            try
            {
                borrowerId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Borrower ID. Please enter a numeric value ");
                return;
            }

            dataAccess.DeleteBorrower(borrowerId);

        }

        static void BorrowBook()  //borrow a book 
        {
            Console.WriteLine("Borrow a Book");

            int bookId;
            Console.Write("Enter Book ID: ");
            try
            {
                bookId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Book ID. Please enter a numeric value");
                return;
            }

            if (!ErrorHandles.IsBookAvailable(bookId)) // validaiton for avaliablilty of book 
            {
                Console.WriteLine("Book is not available for borrowing");
                return;
            }

            int borrowerId;
            Console.Write("Enter Borrower ID: ");
            try
            {
                borrowerId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Borrower ID. Please enter a numeric value");
                return;
            }

            // Validate if the borrower ID exists
            List<Borrower> allBorrowers = dataAccess.GetAllBorrowers();
            bool borrowerExists = false;
            foreach (var borrower in allBorrowers)
            {
                if (borrower.BorrowerId == borrowerId)
                {
                    borrowerExists = true;
                    break;
                }
            }

            if (!borrowerExists)
            {
                Console.WriteLine($"The borrower ID {borrowerId} is not registered.");
                return;
            }

            int transactionId;
            Console.Write("Enter Transaction ID: ");
            try
            {
                transactionId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Transaction ID. Please enter a numeric value.");
                return;
            }

            // Validate if the transaction ID is unique
            if (!ErrorHandles.ValidateTransactionId(transactionId))
            {
                Console.WriteLine("Invalid Transaction ID.");
                return;
            }

            // Record the transaction
            Transaction transaction = new Transaction
            {
                TransactionId = transactionId,
                BookId = bookId,
                BorrowerId = borrowerId,
                Date = DateTime.Now,
                IsBorrowed = true
            };

            dataAccess.RecordTransaction(transaction);

            // Update book availability
            Book borrowedBook = dataAccess.GetBookById(bookId);
            if (borrowedBook != null)
            {
                borrowedBook.IsAvailable = false;
                dataAccess.UpdateBook(borrowedBook);
            }

            Console.WriteLine("Book borrowed successfully");
        }


        static void ReturnBook() // return a book  book id should match with the transactionID and BorrowerID it borrowed to and 
        {
            Console.WriteLine("Return a Book");

            int bookId;
            Console.Write("Enter Book ID: ");
            try
            {
                bookId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Book ID. Please enter a numeric value");
                return;
            }

            int borrowerId;
            Console.Write("Enter Borrower ID: ");
            try
            {
                borrowerId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Borrower ID. Please enter a numeric value");
                return;
            }

            // Validate if the borrower ID exists
            List<Borrower> allBorrowers = dataAccess.GetAllBorrowers();
            bool borrowerExists = false;
            foreach (var borrower in allBorrowers)
            {
                if (borrower.BorrowerId == borrowerId)
                {
                    borrowerExists = true;
                    break;
                }
            }
            if (!borrowerExists)
            {
                Console.WriteLine($"The borrower ID {borrowerId} is not registered.");
                return;
            }

            int transactionId;
            Console.Write("Enter Transaction ID: ");
            try
            {
                transactionId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)  
            {
                Console.WriteLine("Invalid input format for Transaction ID. Please enter a numeric value");
                return;
            }

            // Validate if the transaction ID exists and matches the borrowed transaction
            List<Transaction> borrowedTransactions = dataAccess.GetBorrowedBooksByBorrower(borrowerId);
            bool validTransaction = false;
            foreach (var transaction1 in borrowedTransactions)
            {
                if (transaction1.TransactionId == transactionId && transaction1.BookId == bookId)
                {
                    validTransaction = true;
                    break;
                }
            }

            if (!validTransaction)
            {
                Console.WriteLine($"Invalid Transaction ID for Book ID {bookId} and Borrower ID {borrowerId}.");
                Console.WriteLine("May be BorrowerID or TransactionID not matches with the Borrorw A Book or may book is not borrowed");
                return;
            }

            // Record the transaction
            Transaction transaction = new Transaction
            {
                TransactionId = transactionId,
                BookId = bookId,
                BorrowerId = borrowerId,
                Date = DateTime.Now,
                IsBorrowed = false
            };

            dataAccess.RecordTransaction(transaction);

            // Update book availability
            Book returnedBook = dataAccess.GetBookById(bookId);
            if (returnedBook != null)
            {
                returnedBook.IsAvailable = true;
                dataAccess.UpdateBook(returnedBook);
            }

            Console.WriteLine("Book returned successfully");
        }


        static void SearchBooks() // SEARCHIG BOOKS 
        {
            Console.WriteLine("Search Books");
            Console.Write("Enter search term: ");
            string searchTerm = Menu.GetUserInput();

            List<Book> results = dataAccess.SearchBooks(searchTerm); // MATCHING RESULTS

            Console.WriteLine("Search Results:");
            foreach (Book book in results)
            {
                Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Available: {book.IsAvailable}");
            }
        }

        static void ViewAllBooks()// VIEWING BOOKS 
        {
            Console.WriteLine("View All Books");

            List<Book> books = dataAccess.GetAllBooks();

            foreach (Book book in books)
            {
                Console.WriteLine($"ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Available: {book.IsAvailable}");
            }
        }

        static void ViewBorrowedBooksByBorrowerId() // VIEW BOOK BORROWED BY SPECIFIC BORROWER
        {
            Console.WriteLine("View Borrowed Books by a Specific Borrower");

            int borrowerId;
            Console.Write("Enter Borrower ID: ");
            try
            {
                borrowerId = int.Parse(Menu.GetUserInput());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format for Borrower ID. Please enter a numeric value");
                return;
            }

            List<Transaction> transactions = dataAccess.GetBorrowedBooksByBorrower(borrowerId);

            if (transactions.Count > 0)  // transaction occured 
            {
                Console.WriteLine($"Books borrowed by Borrower ID {borrowerId} in the past (but returned) or still borrowed are :");
                foreach (var transaction in transactions)
                {
                    Book book = dataAccess.GetBookById(transaction.BookId);
                    Console.WriteLine($"Book ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Borrowed On: {transaction.Date}");
                }
            }
            else
            {
                Console.WriteLine("No books borrowed by this borrower ");
            }
        }
    }
}