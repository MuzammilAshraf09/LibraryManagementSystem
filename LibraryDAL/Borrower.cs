namespace LibraryDAL
{
    public class Borrower
    {
        // fields
        int borrowerId;
        string name;
        string email;

        // properties to access feilds from outside the class 
        
        public int BorrowerId{
            get { 
                return borrowerId; 
            }
            set {
                borrowerId = value;
            }
        }

        public string Name{
            get {
                return name; 
            }
            set {
                name = value; 
            }
        }

        public string Email{
            get { 
                return email; 
            }
            set { 
                email = value; 
            }
        }
    }

}