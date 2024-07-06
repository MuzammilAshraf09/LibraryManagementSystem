namespace LibraryDAL
{
    public class Book
    {
        // fields 
        int bookId;
        string title;
        string author;
        string genre;
        bool isAvailable;

        // properties to access feilds from outside the class 
        public int BookId{
            get{
                return bookId;
            }
            set {
                bookId= value;
            }

        }
        public string Title{
            get{
                return title;
            }
            set{
                title= value;
            }
        }
        public string Author{
            get{
                return author;
            }
            set{
                author = value;
            }
        }
        public string Genre{
            get {
                return genre;
            }
            set{
                genre= value; 
            }
        }
        public bool IsAvailable{
            get{
                return isAvailable;
            }
            set {
                isAvailable= value;
            }
        }
        
    }
}