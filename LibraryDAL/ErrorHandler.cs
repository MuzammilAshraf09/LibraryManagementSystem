using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryDAL
{
    public static class ErrorHandles
    {
        public static bool ValidateBookId(int bookId) //validate BookID
        {
            if (bookId <= 0)
            {
                Console.WriteLine("Book ID should be a positive integer.");
                return false;
            }

            // Check if the Book ID is unique
            DataAccess dataAccess = new DataAccess();
            List<Book> books = dataAccess.GetAllBooks();
            foreach (Book book in books)
            {
                if (book.BookId == bookId)
                {
                    Console.WriteLine("Book ID already exists.");
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateBorrowerId(int borrowerId) // validate borrowersID
        {
            if (borrowerId <= 0)
            {
                Console.WriteLine("Borrower ID should be a positive integer.");
                return false;
            }

            // Check if the Borrower ID is unique
            DataAccess dataAccess = new DataAccess();
            List<Borrower> borrowers = dataAccess.GetAllBorrowers();
            foreach (Borrower borrower in borrowers)
            {
                if (borrower.BorrowerId == borrowerId)
                {
                    Console.WriteLine("Borrower ID already exists.");
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateTransactionId(int transactionId) // validate transactions 
        {
            if (transactionId <= 0)
            {
                Console.WriteLine("Transaction ID should be a positive integer.");
                return false;
            }

            // Check if the Transaction ID is unique
            DataAccess dataAccess = new DataAccess();
            List<Transaction> transactions = dataAccess.GetAllTransactions();
            foreach (Transaction transaction in transactions)
            {
                if (transaction.TransactionId == transactionId)
                {
                    Console.WriteLine("Transaction ID already exists.");
                    return false;
                }
            }

            return true;
        }

        public static bool ValidateEmailFormat(string email) // prevent registration of borrorwers 
        {
            if (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email format.");
                return false;
            }
            return true;
        }

        public static bool IsBookAvailable(int bookId)  //validate availability of book 
        {
            DataAccess dataAccess = new DataAccess();
            List<Book> books = dataAccess.GetAllBooks();
            foreach (Book book in books)
            {
                if (book.BookId == bookId)
                {
                    if (!book.IsAvailable)
                    {
                        Console.WriteLine($"Book with ID {bookId} is not available for borrowing.");
                        return false;
                    }
                    return true;
                }
            }

            Console.WriteLine($"Book with ID {bookId} not found.");
            return false;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
