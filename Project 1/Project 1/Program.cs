using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Project_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Record> records = new List<Record>();
            Block b = new Block(records, 100);

            Record r = new Record("abc", 5.6, 1000);
            b.addNewRecord(r);
            
            //doesn't work
            //Console.WriteLine(b.getBlockSize()>Marshal.SizeOf(b));
        }
    }
}