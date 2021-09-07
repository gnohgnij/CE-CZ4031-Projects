using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Project_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Record> records = new List<Record>();
            Block b = new Block(records);

            Record r = new Record("abc", 5.6, 1000);
            Record er = new Record("def", 4.321, 1000);
            b.addNewRecord(r);
            b.addNewRecord(er);
            b.getRecords();

            // Console.WriteLine(b.getRecords());
        }
    }
}