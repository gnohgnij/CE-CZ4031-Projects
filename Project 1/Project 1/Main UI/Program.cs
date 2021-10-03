using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace Project_1
{
    class Program
    {
        public Disk readAllTuples(int blockSize)
        {
            List<Block> blocks = new List<Block>();
            int j = 0;
            int blockID = 1;
            List<Record> temp = new List<Record>();
            using (var reader = new StreamReader("C:\\Users\\jingh\\Desktop\\CE-CZ4031-Projects\\Project 1\\Project 1\\testing.tsv"))
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
                        char[] tconstArray = new char[10];
                        for (int i = 0; i < values[0].Length; i++)
                        {
                            tconstArray[i] = values[0][i];
                        }
                        Record r = new Record(tconstArray, Double.Parse(values[1]), int.Parse(values[2]));
                        if (blocks.Count == 0)
                        {
                            blocks.Add(new Block(new List<Record>(), blockSize, blockID));
                            r.setBlockID(blockID);
                            blocks[j].addNewRecord(r);

                            //r.printRecord();
                            //Console.WriteLine(r.getBlockID());
                        }
                        else if (blocks[j].getAvailableSpace() > r.getBytes())
                        {
                            r.setBlockID(blockID);
                            blocks[j].addNewRecord(r);
                            //r.printRecord();
                            //Console.WriteLine(r.getBlockID());
                        }
                        else if (blocks[j].getAvailableSpace() < r.getBytes())
                        {
                            j++;
                            blockID++;
                            blocks.Add(new Block(new List<Record>(), blockSize, blockID));

                            blocks[j].printRecords();
                            r.setBlockID(blockID);
                            blocks[j].addNewRecord(r);
                            //r.printRecord();
                            //Console.WriteLine(r.getBlockID());
                        }      
                    }
                }
            }
            Console.WriteLine("Total number of blocks created = " + blocks.Count);
            return new Disk(blocks);
        }

        public Disk recordsIntoDisk(List<Record> listOfRecords, int blockSize)
        {
            
            List<Block> blocks = new List<Block>();
            int j = 0;
            int blockID = 10;
            for (int i = 0; i < listOfRecords.Count; i++)
            {
                if (blocks.Count == 0)
                {
                    blocks.Add(new Block(new List<Record>(), blockSize, blockID));
                    listOfRecords[i].setBlockID(blockID);
                    blocks[j].addNewRecord(listOfRecords[i]);

                    listOfRecords[i].printRecord();
                    Console.WriteLine(listOfRecords[i].getBlockID());
                }
                else if (blocks[j].getAvailableSpace() > listOfRecords[0].getBytes())
                {
                    listOfRecords[i].setBlockID(blockID);
                    blocks[j].addNewRecord(listOfRecords[i]);
                    listOfRecords[i].printRecord();
                    Console.WriteLine(listOfRecords[i].getBlockID());
                }
                else if (blocks[j].getAvailableSpace() < listOfRecords[0].getBytes())
                {
                    j++;
                    blockID++;
                    blocks.Add(new Block(new List<Record>(), blockSize, blockID));
                    
                    blocks[j].printRecords();
                    listOfRecords[i].setBlockID(blockID);
                    blocks[j].addNewRecord(listOfRecords[i]);
                    listOfRecords[i].printRecord();
                    Console.WriteLine(listOfRecords[i].getBlockID());
                }
            }
            Console.WriteLine("Total number of blocks created = " + blocks.Count);
            return new Disk(blocks);
        }
        
        public Disk start(int blockSize)
        {
          return readAllTuples(blockSize);
        }
        
        
    }
}