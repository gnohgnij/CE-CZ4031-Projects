﻿using System;
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
            using (var reader = new StreamReader("C:\\Users\\jingh\\Desktop\\CE-CZ4031-Projects\\Project 1\\Project 1\\data.tsv"))    //change for actual tsv file
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
            Console.WriteLine("Total number of records created = " + temp.Count);
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
            Console.WriteLine("Total number of blocks created = " + blocks.Count);
            return new Disk(blocks);
        }
        
        public Disk start(int blockSize)
        {
            List<Record> list = readAllTuples();
            return recordsIntoDisk(list, blockSize);
        }
        
        
    }
}