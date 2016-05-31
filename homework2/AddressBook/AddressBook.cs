using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

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

        //homework2
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        //homework3

        public void SelectByEmail()
        {
            string pattern = @".*@gmail\.com";
            var querry =
                from book in Book
                where Regex.Match(book.Email, pattern, RegexOptions.IgnoreCase).Success == true
                select new
                {
                    FirstName = book.FirstName,
                    LastName = book.LastName,
                    Email = book.Email,
                };
            foreach (var item in querry)
                Console.WriteLine("{0} {1} {2} ", item.FirstName, item.LastName, item.Email);
        }

        public IEnumerable SelectAdultsExt()
        {
            return Book.SelectAdultsExtension();
        }

        public void SelectByGender()
        {
            var querry =
                from book in Book
                where book.Gender == "female"
                where DateTime.Now.Subtract(book.TimeAdded).Days <= 10
                select new
                {
                    FirstName = book.FirstName,
                    SecondName = book.LastName,
                    TimeAdded = book.TimeAdded,
                };

            foreach (var item in querry)
                Console.WriteLine("{0} {1} {2}", item.FirstName, item.SecondName, item.TimeAdded.Date.ToString("yyyy-MM-dd"));
        }

        public IEnumerable SelectByJanuary()
        {
            var querry =
                from book in Book
                where book.Birthdate.Month == 1
                where !book.Address.Equals("")
                where !book.PhoneNumber.Equals("")
                orderby book.LastName descending
                select new
                {
                    FirstName = book.FirstName,
                    SecondName = book.LastName,
                    Address = book.Address,
                    PhoneNumber = book.PhoneNumber
                };
            foreach (var item in querry)
                yield return item;
        }

        public Dictionary<string, IEnumerable> SelectDictionary()
        {
            Dictionary<string, IEnumerable> dictionary = new Dictionary<string, IEnumerable>();
            var querryMan =
                from book in Book
                where book.Gender == "male"
                select new
                {
                    FirstName = book.FirstName,
                    LastName = book.LastName
                };
            var querryWoman =
                from book in Book
                where book.Gender == "female"
                select new
                {
                    FirstName = book.FirstName,
                    LastName = book.LastName
                };
            dictionary.Add("man", querryMan);
            dictionary.Add("woman", querryWoman);
            return dictionary;
        }


        public IEnumerable SelectRange(int startPos, int endPos)
        {
            var querry =
                Book
                .Skip(startPos)
                .Take(endPos)
                .Where(book => book.Gender == "male")
                .Select(book => new
                {
                    FirstName = book.FirstName,
                    LastName = book.LastName
                });
            foreach (var item in querry)
                yield return item;
        }


        public int SelectBirthdateCount(string city)        // Немедленное выполнение LINQ запроса.
        {
            return(                     
                from book in Book
                where DateTime.Now.Month == book.Birthdate.Month
                where DateTime.Now.Day == book.Birthdate.Day
                where book.City == city
                select book).Count();
            
        }
    }

    static class ExtensionClass                     
    {
        public static IEnumerable SelectAdultsExtension(this List<User> Book)       //Метод расширения для типа List<User>
        {
            var querry =
                from book in Book
                where book.City == "Kyiv"
                where DateTime.Now.Subtract(book.TimeAdded).TotalDays <= 18 * 365.25
                select new
                {
                    FirstName = book.FirstName,
                    LastName = book.LastName
                };
            foreach (var item in querry)
                yield return item;
        }
    }
}
