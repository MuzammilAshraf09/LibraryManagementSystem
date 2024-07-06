// to perform the CRUD operations for the Library (BOOks,Borrowers,Transactions)

using System;
using System.IO;
using System.Collections.Generic;

namespace LibraryDAL
{
    public class DataAccess
    {
        // Methods for the BOOk CRUD Operations
        public void AddBook(Book book)  // Add a new Book to the File 
        {
            FileStream fStream = new FileStream("books.txt", FileMode.Append); // file openinig to append
            StreamWriter writer = new StreamWriter(fStream); // for writing to file

            writer.WriteLine($"{book.BookId},{book.Title},{book.Author},{book.Genre},{book.IsAvailable}");

            writer.Close();
            fStream.Close();
        }

        public void RemoveBook(int bookId) // to remove a bool from library 
        {
            List<Book> books = GetAllBooks();  // bbooks in Library
            int index = -1;   // index of book to remove frm list
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].BookId == bookId)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                books.RemoveAt(index);
                SaveBooksToFile(books);  // save remaining books to file
            }
            else
            {
                Console.WriteLine($"Book with ID {bookId} not found"); 
            }
        }

        public void UpdateBook(Book book) // update an existing book 
        {
            List<Book> books = GetAllBooks();

            bool flag = false;  // flag for the book have been find or not    
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].BookId == book.BookId)
                {
                    books[i].Title = book.Title;
                    books[i].Author = book.Author;
                    books[i].Genre = book.Genre;
                    books[i].IsAvailable = book.IsAvailable;
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                SaveBooksToFile(books); // save books with updated book
            }
            else
            {
                Console.WriteLine($"Book with ID {book.BookId} not found ");
            }
        }

        public List<Book> GetAllBooks() // get all books in library 
        {
            FileStream fStream = new FileStream("books.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(fStream);

            List<Book> books = new List<Book>(); // to store all books in library
            string lineData;
            while ((lineData= reader.ReadLine()) != null)
            {
                string[] parts = lineData.Split(',');
                int bookId = int.Parse(parts[0]);
                string title = parts[1];
                string author = parts[2];
                string genre = parts[3];
                bool isAvailable = bool.Parse(parts[4]);
                // adding book to books 
                books.Add(new Book { BookId = bookId, Title = title, Author = author, Genre = genre, IsAvailable = isAvailable });


            }

            reader.Close();
            fStream.Close();

            return books;
        }

        public Book GetBookById(int bookId)  // retriving a specific book by id 
        {
            List<Book> books = GetAllBooks();  
            foreach (var book in books)
            {
                if (book.BookId == bookId)
                {
                    return book;
                }
            }
            return null; // no book by ID
        }

        public List<Book> SearchBooks(string query)  // serach books by Title or Author ,Genre
        {
            List<Book> books = GetAllBooks();
            List<Book> results = new List<Book>();

            string lowerQuery = query.ToLower();

            foreach (var book in books)
            {
                string lowerTitle = book.Title.ToLower();
                string lowerAuthor = book.Author.ToLower();
                string lowerGenre = book.Genre.ToLower();

                if (lowerTitle.Contains(lowerQuery) || lowerAuthor.Contains(lowerQuery) || lowerGenre.Contains(lowerQuery))
                {
                    results.Add(book);
                }
            }

            return results;
        }


        public void RegisterBorrower(Borrower borrower)  // register a new boororwer of book 
        {
            FileStream fStream = new FileStream("borrowers.txt", FileMode.Append);
            StreamWriter writer = new StreamWriter(fStream);

            writer.WriteLine($"{borrower.BorrowerId},{borrower.Name},{borrower.Email}");

            writer.Close();
            fStream.Close();
        }

        public void UpdateBorrower(Borrower borrower) //update an existing borrorwer
        {
            List<Borrower> borrowers = GetAllBorrowers();
            bool flag = false;
            for (int i = 0; i < borrowers.Count; i++)
            {
                if (borrowers[i].BorrowerId == borrower.BorrowerId)
                {
                    borrowers[i].Name = borrower.Name;
                    borrowers[i].Email = borrower.Email;
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                SaveBorrowersToFile(borrowers);
            }
            else
            {
                Console.WriteLine($"Borrower with ID {borrower.BorrowerId} not found.");
            }
        }

        public void DeleteBorrower(int borrowerId)  // delete a borrower
        {
            List<Borrower> borrowers = GetAllBorrowers();
            int index = -1; 
            for (int i = 0; i < borrowers.Count; i++)
            {
                if (borrowers[i].BorrowerId == borrowerId)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                borrowers.RemoveAt(index);
                SaveBorrowersToFile(borrowers);
            }
            else
            {
                Console.WriteLine($"Borrower with ID {borrowerId} not found.");
            }
        }

        public void RecordTransaction(Transaction transaction) // record transsaction
        {
            FileStream fStream = new FileStream("transactions.txt", FileMode.Append);
            StreamWriter writer = new StreamWriter(fStream);

            writer.WriteLine($"{transaction.TransactionId},{transaction.BookId},{transaction.BorrowerId},{transaction.Date},{transaction.IsBorrowed}");

            writer.Close();
            fStream.Close();
        }

        public List<Transaction> GetBorrowedBooksByBorrower(int borrowerId)   //getBorrowerbyID
        {
            List<Transaction> transactions = GetAllTransactions();
            List<Transaction> borrowedBooks = new List<Transaction>();

            foreach (var transaction in transactions)
            {
                if (transaction.BorrowerId == borrowerId && transaction.IsBorrowed)
                {
                    borrowedBooks.Add(transaction);
                }
            }

            return borrowedBooks;
        }

        public List<Borrower> GetAllBorrowers()
        {
            FileStream fStream = new FileStream("borrowers.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(fStream);

            List<Borrower> borrowers = new List<Borrower>();
            string lineData;
            while ((lineData = reader.ReadLine()) != null)
            {
                string[] parts = lineData.Split(',');
                int borrowerId = int.Parse(parts[0]);
                string name = parts[1];
                string email = parts[2];

                borrowers.Add(new Borrower { BorrowerId = borrowerId, Name = name, Email = email });
            }

            reader.Close();
            fStream.Close();

            return borrowers;
        }

        public List<Transaction> GetAllTransactions() // getA
        {
            FileStream fStream = new FileStream("transactions.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(fStream);

            List<Transaction> transactions = new List<Transaction>();
            string lineData;
            while ((lineData = reader.ReadLine()) != null)
            {
                string[] parts = lineData.Split(',');
                int transactionId = int.Parse(parts[0]);
                int bookId = int.Parse(parts[1]);
                int borrowerId = int.Parse(parts[2]);
                DateTime date = DateTime.Parse(parts[3]);
                bool isBorrowed = bool.Parse(parts[4]);

                transactions.Add(new Transaction { TransactionId = transactionId, BookId = bookId, BorrowerId = borrowerId, Date = date, IsBorrowed = isBorrowed });
            }

            reader.Close();
            fStream.Close();

            return transactions;
        }

        private void SaveBooksToFile(List<Book> books) // save books to file after some operations
        {
            FileStream fStream = new FileStream("books.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fStream);

            foreach (var book in books)
            {
                writer.WriteLine($"{book.BookId},{book.Title},{book.Author},{book.Genre},{book.IsAvailable}");
            }

            writer.Close();
            fStream.Close();
        }

        private void SaveBorrowersToFile(List<Borrower> borrowers)  //save borrowers to file after some operations
        {
            FileStream fStream = new FileStream("borrowers.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fStream);

            foreach (var borrower in borrowers)
            {
                writer.WriteLine($"{borrower.BorrowerId},{borrower.Name},{borrower.Email}");
            }

            writer.Close();
            fStream.Close();
        }
    }
}