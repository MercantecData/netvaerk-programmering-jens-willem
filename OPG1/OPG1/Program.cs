using System;
using System.Text;

namespace OPG1
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Here you have a string! ÅÆØ";
            var bytes = Encoding.UTF8.GetBytes(text);
            text = Encoding.UTF8.GetString(bytes);
            Console.WriteLine(text);
        }
    }
}
