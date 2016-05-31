using System;
using AddressBook;
using Logger;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressBook.AddressBook Book = AddressBook.AddressBook.Instance(); //singleton
            object[] obj = new object[] { "Vailiev", "Ivan", new DateTime(1999, 10, 11), new DateTime(2006, 10, 10), "Kyiv", "no address", "0854751145", "male", "binary@mail.ru" };
            object[] obj2 = new object[] { "Shulga", "Dmutro", new DateTime(2001, 10, 11), new DateTime(2006, 10, 10), "Kyiv", "no address", "0854751145", "male", "binary@mail.ru" };
            object[] obj3 = new object[] { "Kolesnik", "Anna", new DateTime(1993, 1, 11), new DateTime(2016, 5, 24), "Odessa", "no address", "0854751145", "female", "random@gmail.com" };

            Book.UserAdded += new BookEvent(Added);
            Book.UserDeleted += new BookEvent(Deleted);
        
            Book.AddUser(obj);
            Book.AddUser(obj2);
            Book.AddUser(obj3);
            Book.AddUser("Ivanova", "Olga", new DateTime(1995, 1, 3), new DateTime(2016, 5, 28), "Lviv", "no address", "0954569874", "female", "inout@gmail.com");
            Console.WriteLine(new string('-', 50));

            Book.SelectByEmail();
            Console.WriteLine(new string('-', 50));

            var  AdultsFromKyiv= Book.SelectAdultsExt();
            foreach (var item in AdultsFromKyiv)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(new string('-', 50));

            Book.SelectByGender();
            Console.WriteLine(new string('-', 50));

            var SelectJanuaryTemp = Book.SelectByJanuary();
            foreach (var item in SelectJanuaryTemp)
                Console.WriteLine(item);
            Console.WriteLine(new string('-', 50));

            Dictionary<string, IEnumerable> dictionary = Book.SelectDictionary();
            var dictionaryTemp = dictionary["man"];
            foreach (var item in dictionaryTemp)
                Console.WriteLine(item);
            Console.WriteLine(new string('-', 50));

            var SelectRangeTemp = Book.SelectRange(1, 4);
            foreach (var item in SelectRangeTemp)
                Console.WriteLine(item);
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("The number of people whose birthday is today: " + Book.SelectBirthdateCount("Kyiv"));
            Console.WriteLine(new string('-', 50));
            Book.RemoveUser((User)obj);
            Book.RemoveUser(0); // via index
            Console.ReadKey();
        }

        private static void Added()
        {
            Logger.Logger log = Logger.Logger.Instance();
            log.Info("One more User added!");
            log.Warning(true);
        }

        private static void Deleted()
        {
            Logger.Logger log = Logger.Logger.Instance();
            log.Info("User was deleted!");
            log.Warning(false);
        }
    }
}
