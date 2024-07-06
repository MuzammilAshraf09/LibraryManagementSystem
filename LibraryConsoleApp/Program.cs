using System;
using System.Collections.Generic;
using LibraryDAL;
namespace LibraryConsoleApp
{
    class Program
    {
        static DataAccess dataAccess = new DataAccess();

        private static void Main(string[] args)
        {
            dataAccess.GetAllBooks();
            dataAccess.GetAllBorrowers();
            dataAccess.GetBorrowedBooksByBorrower(2);
        }
    }
}