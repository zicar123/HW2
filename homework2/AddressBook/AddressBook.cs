using System;
using System.Collections.Generic;

namespace AddressBook
{
    public delegate void BookEvent();

    public class AddressBook
    {
        private List<User> Book = new List<User>();
        public event BookEvent UserAdded;
        public event BookEvent UserDeleted;

        #region Singleton
        private static AddressBook instance = null;

        protected AddressBook() 
        { 
        }

        public static AddressBook Instance()        
        {
            if (instance == null)
            {
                instance = new AddressBook();
            }
            return instance;
        }
        #endregion

        public void AddUser(params object[] data)
        {
            this.Book.Add((User)data);
            UserAdded.Invoke();
        }

        public void RemoveUser(int index)
        {       
            try
            {
                this.Book.RemoveAt(index);
                UserDeleted.Invoke();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("No such index. Can't delete.");
            }
        }

        #region overloads of AddUser\RemoveUser

        public void AddUser(string LastName, string FirstName, DateTime Birthdate,
            DateTime TimeAdded, string City, string Address, string PhoneNumber,
            string Gender, string Email)
        {
            User user = User.Fabric();
            user.LastName = LastName;
            user.FirstName = FirstName;
            user.Birthdate = Birthdate;
            user.TimeAdded = TimeAdded;
            user.City = City;
            user.Address = Address;
            user.PhoneNumber = PhoneNumber;
            user.Gender = Gender;
            user.Email = Email;
            this.Book.Add(user);
            UserAdded.Invoke();
        }

        public void RemoveUser(User user)
        {
            this.Book.Remove(user);
            UserDeleted.Invoke();
        }
        #endregion
    }
}
