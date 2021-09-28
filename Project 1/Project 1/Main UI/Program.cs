using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace Project_1
{
    class Program
    {
        public List<Record> readAllTuples()
        {
            List<Record> temp = new List<Record>();
            using (var reader = new StreamReader("C:\\Users\\jeral\\OneDrive\\Desktop\\DSP\\Project 1\\Project 1\\testing.tsv"))    //change for actual tsv file
            {
                bool firstLine = true;
                while (!reader.EndOfStream)
                {
                    string tuples = reader.ReadLine();
                    if (firstLine == true)
                    {
                        firstLine = false;
                    }
                    else
                    {
                        string[] values = tuples.Split("\t");
                        char[] tconstArray = new char[7];
                        for (int i = 0; i < tconstArray.Length; i++)
                        {
                            tconstArray[i] = values[0][i];
                        }
                        Record r = new Record(tconstArray, Double.Parse(values[1]), int.Parse(values[2]));
                        temp.Add(r);
                    }
                }
            }
            Console.WriteLine("Total number of records = " + temp.Count);
            return temp;
        }

        public Disk recordsIntoDisk(List<Record> listOfRecords, int blockSize)
        {
            List<Block> blocks = new List<Block>();
            int j = 0;
            for (int i = 0; i < listOfRecords.Count; i++)
            {
                if (blocks.Count == 0)
                {
                    blocks.Add(new Block(new List<Record>(), blockSize));
                    blocks[j].addNewRecord(listOfRecords[i]);
                    // Console.WriteLine("Block no." + j+1 + ", record no." + i+1);
                }
                else if (blocks[j].getAvailableSpace() > listOfRecords[0].getBytes())
                {
                    blocks[j].addNewRecord(listOfRecords[i]);
                    // Console.WriteLine("Block no." + j+1 + ", record no." + i+1);
                }
                else if (blocks[j].getAvailableSpace() < listOfRecords[0].getBytes())
                {
                    j++;
                    blocks.Add(new Block(new List<Record>(), blockSize));
                    blocks[j].addNewRecord(listOfRecords[i]);
                    // Console.WriteLine("Block no." + j+1 + ", record no." + i+1);
                }
            }
            Console.WriteLine("Total number of blocks = " + blocks.Count);
            return new Disk(blocks);
        }
        
        public Disk start(int blockSize)
        {
            List<Record> list = readAllTuples();
            return recordsIntoDisk(list, blockSize);
        }      

        public void test()
        {
            List<LeafNode> l1 = new List<LeafNode>();
            Record r1 = new Record(new char[]{'a', 'b'}, 5.4, 0);
            Record r2 = new Record(new char[]{'a', 'b'}, 5.4, 1);
            Record r3 = new Record(new char[]{'a', 'b'}, 5.4, 2);
            Record r4 = new Record(new char[]{'a', 'b'}, 5.4, 3);

            BPlusTree b = new BPlusTree();
            l1.Add(new LeafNode(new List<int>(), new List<Record>()));
            b.insert(r1.getNumVotes(), r1);
            b.insert(r2.getNumVotes(), r2);
            b.insert(r3.getNumVotes(), r3);
            b.insert(r4.getNumVotes(), r4);
        }
    }
}