using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Project_1
{
    public struct Block
    {
        private List<Record> records;
        private int blockSize;
        private int blockID;

        public Block(List<Record> records, int blockSize, int blockID)
        {
            this.records = records;

            if (records.Count > 1)
            {
                // sort the records based on their tconst in ascending order
                // convert the tconst array to string first then compare
                records.Sort((r1, r2) => new String(r1.getTConst()).CompareTo(new String(r2.getTConst())));    
            }

            this.blockSize = blockSize;
            this.blockID = blockID;
        }

        public int getBlockSize()
        {
            return this.blockSize;
        }

        public int getBlockID()
        {
            return this.blockID;
        }
        
        public void setBlockID(int blockID)
        {
            this.blockID = blockID;
        }
        
        public int getBytes()
        {
            int totalBytes = 0;
            foreach (var record in records) 
            {
                totalBytes += record.getBytes();
            }
            return totalBytes;
        }
        
        public List<Record> getRecords()
        {
            return records;
        }

        public void printRecords()
        {
            foreach (var record in records) 
            {
                Console.Write(record.getTConst() + " " + record.getAverageRating() + " " + record.getNumVotes());
            }
        }

        public int getAvailableSpace()
        {
            return (getBlockSize() - getBytes());
        }
        
        public void addNewRecord(Record record)
        { 
            records.Add(record);

            if (records.Count > 1)
            {
                // sort the records based on their tconst in ascending order
                // convert the tconst array to string first then compare
                records.Sort((r1, r2) => new String(r1.getTConst()).CompareTo(new String(r2.getTConst())));
            }
        }

        public int getNumberOfRecords()
        {
            return records.Count;
        }

        public string getSmallestTConst()
        {
            // assuming list already sorted in ascending order of tconst
            // convert tconst char array to string
            return new String(records[0].getTConst());
        }
    }
}