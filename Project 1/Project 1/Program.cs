using System;
using System.Runtime.InteropServices;

namespace Project_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Record r = new Record("abc", 4, 100);
            Record a = new Record("def", 5, 50);
            Console.WriteLine("Size of r = " + Marshal.SizeOf(r));
            Console.WriteLine("Size of a = " + Marshal.SizeOf(a));
        }
    }
}