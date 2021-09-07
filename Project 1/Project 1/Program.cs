using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Project_1
{
    // automate parsing in / resolve other conflicts
    class Program
    {
        public static List<Record> records = new List<Record>();
        public static List<Block> blocks = new List<Block>();
        public static Disk disk = new Disk(blocks);

        public static void Main(string[] args)
        {   
            Block block = new Block(records, 100);

            block.addNewRecord(new Record("Sinkhole", 7.8, 1000)); //15B
            block.addNewRecord(new Record("The Medium", 8.8, 1000)); //17B
            block.addNewRecord(new Record("Parasite", 10.0, 1000)); //16B

            Record test = new Record("Parasite", 10.1, 1000);
            Console.WriteLine("Record Size:" + test.getBytes());

            disk.addNewBlock(new Block(records, 100));
            Console.WriteLine("Disk Size:" + disk.noOfBlocks());

            block.getRecords();
            Console.WriteLine("Block Size:" + block.getBlockSize());
            Console.WriteLine("No of Records:" + block.getNumberOfRecords());

        } 
    }
}