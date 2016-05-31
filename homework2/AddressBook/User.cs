using System;

namespace AddressBook
{
    public class User
    {
        #region fields
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime TimeAdded { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        #endregion

        public User()
        {
            this.LastName = "";
            this.FirstName = "";
            this.Birthdate = default(DateTime);
            this.TimeAdded = default(DateTime);
            this.City = "";
            this.Address = "";
            this.PhoneNumber = "";
            this.Gender = "";
            this.Email = "";
        }

        public static User Fabric()
        {
            return new User();
        }



        public static explicit operator User(object[] list)
        {
            User user = User.Fabric();
            try
            {
                user.LastName = (String)list[0];
                user.FirstName = (String)list[1];
                user.Birthdate = (DateTime)list[2];
                user.TimeAdded = (DateTime)list[3];
                user.City = (String)list[4];
                user.Address = (String)list[5];
                user.PhoneNumber = (String)list[6];
                user.Gender = (String)list[7];
                user.Email = (String)list[8];
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Index out of range. \n" + e.Message);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("Error while converting types. \n" + e.Message);
            }
            return user;
        }
    }
}
