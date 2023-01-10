using DAL.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // dal project...

            DbTest1();
        }

        private static void DbTest1()
        {
            var apartments = DbAccess.LoadApartments();

            foreach (var a in apartments)
            {
                Console.WriteLine(a.Name);
            }
        }
    }
}
