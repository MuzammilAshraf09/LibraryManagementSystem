namespace LibraryDAL
{
    public class Transaction
{
    // fields
    int transactionId;
    int bookId;
    int borrowerId;
    DateTime date;
    bool isBorrowed;

    // properties to access feilds from outside the class 

    public int TransactionId{
        get{ 
            return transactionId; 
        }
        set{ 
            transactionId = value; 
        }
    }

    public int BookId{
        get{ 
        return bookId; 
        }
        set{ 
        bookId = value; 
        }
    }

    public int BorrowerId
    {
        get{ 
            return borrowerId; 
        }
        set{ 
            borrowerId = value; 
        }
    }

    public DateTime Date
    {
        get{ 
            return date; 
        }
        set { 
            date = value; 
        }
    }

    public bool IsBorrowed
    {
        get{ 
            return isBorrowed; 
        }
        set{ 
            isBorrowed = value; 
        }
    }
}

} 
