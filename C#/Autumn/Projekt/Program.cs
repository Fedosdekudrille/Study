using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    static class Extensions
    {
        public static void Rand(this Random random)
        {
            Console.WriteLine(random.Next());
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
           Random random = new Random();
            random.Rand();
        }
    }
}
