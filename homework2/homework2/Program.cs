using System;
using AddressBook;
using Logger;

namespace homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressBook.AddressBook book = AddressBook.AddressBook.Instance(); //singleton
            object[] obj = new object[] { "Vailiev", "Ivan", new DateTime(1996, 10, 11), new DateTime(2016, 1, 10), "Kiev", "-", "0854751145", "Male", "binary@mail.ru" };

            book.UserAdded += new BookEvent(Added);
            book.UserDeleted += new BookEvent(Deleted);

            book.AddUser(obj);
            book.AddUser("Ivanova", "Olga", new DateTime(1995, 8, 3), new DateTime(2015, 5, 8), "Lvov", "-", "0954569874", "Female", "inout@mail.ru");
            book.RemoveUser((User)obj);
            book.RemoveUser(0); // via index

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
