using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace Project_1
{
    // automate parsing in / resolve other conflicts
    class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("hello world");
            /*
            * This part reads the tsv file and creates records per row
            */
            
            // using (var reader =
            //     new StreamReader(""))   // change to your own directory
            // {
            //     bool firstLine = true;
            //     while (!reader.EndOfStream)
            //     {
            //         string tuples = reader.ReadLine(); //Process row
            //         if (firstLine == true)
            //         {
            //             firstLine = false;
            //         }
            //         else
            //         {
            //             string[] values = tuples.Split("\t");
            //             char[] tconstArray = new char[7];
            //             for (int i = 0; i < tconstArray.Length; i++)
            //             {
            //                 tconstArray[i] = values[0][i];
            //             }

            //             Record r = new Record(tconstArray, Double.Parse(values[1]), Int32.Parse(values[2]));
            //             Console.WriteLine(r.getBytes());
            //             recordList.Add(r);
            //         }
            //     }
            // }
            
            // List<Record> records1 = new List<Record>();
            // Record r1 = new Record("t001", 7.9, 123);
            // Record r2 = new Record("t002", 6.0, 430);
            // Record r3 = new Record("t003", 6.4, 499);
            // records1.Add(r1);
            // records1.Add(r2);
            // records1.Add(r3);
            // Block b1 = new Block(records1, 100);
            //
            // List<Record> records2 = new List<Record>();
            // Record r4 = new Record("t004", 7.9, 123);
            // Record r5 = new Record("t005", 6.0, 430);
            // Record r6 = new Record("t006", 6.4, 499);
            // records2.Add(r4);
            // records2.Add(r5);
            // records2.Add(r6);
            // Block b2 = new Block(records2, 100);
            //
            // List<Block> blocks = new List<Block>();
            // blocks.Add(b1);
            // blocks.Add(b2);
            // Disk d1 = new Disk(blocks);
            //
            // d1.getBlocks();
        } 
    }
}