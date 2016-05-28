using System;
using System.IO;

namespace Logger
{
    public class Logger
    {
        #region Singleton
        private static Logger instance = null;

        protected Logger()
        {
        }

        public static Logger Instance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }
        #endregion

        public void Info(string text)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                StreamWriter output = File.AppendText(path + "\\Info.log");
                output.WriteLine(text + " | " + DateTime.Now);
                output.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while working with file. \n" + e.Message);
            }
        }

        public void Warning(bool flag)
        {
            if (flag) Console.WriteLine(" One More User Added! " + " | " + DateTime.Now);
            else Console.WriteLine(" User was removed! " + " | " + DateTime.Now);
        }

        public void Error()
        {
            //из задания не понятно для чего нужны эти методы
        }

        public void Debug()
        {
        }

    }
}
