using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Project_1
{
    // automate parsing in / resolve other conflicts
    class Program
    {

        public static void Main(string[] args)
        {
            List<Record> records1 = new List<Record>();
            Record r1 = new Record("t001", 7.9, 123);
            Record r2 = new Record("t002", 6.0, 430);
            Record r3 = new Record("t003", 6.4, 499);
            records1.Add(r1);
            records1.Add(r2);
            records1.Add(r3);
            Block b1 = new Block(records1, 100);
            
            List<Record> records2 = new List<Record>();
            Record r4 = new Record("t004", 7.9, 123);
            Record r5 = new Record("t005", 6.0, 430);
            Record r6 = new Record("t006", 6.4, 499);
            records2.Add(r4);
            records2.Add(r5);
            records2.Add(r6);
            Block b2 = new Block(records2, 100);

            List<Block> blocks = new List<Block>();
            blocks.Add(b1);
            blocks.Add(b2);
            Disk d1 = new Disk(blocks);

            d1.getBlocks();

        } 
    }
}