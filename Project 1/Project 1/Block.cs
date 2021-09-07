using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Project_1
{
    public struct Block
    {
        private List<Record> records;
        private int blockSize;

        public Block(List<Record> records, int blockSize)
        {
            this.records = records;

            if (records.Count > 1)
            {
                // sort the records based on their numVotes in ascending order
                records.Sort((r1, r2) => r1.getNumVotes().CompareTo(r2.getNumVotes()));    
            }

            this.blockSize = blockSize;
        }

        public int getBlockSize()
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
            foreach (var record in records) 
            {
                Console.WriteLine(record.getTConst() + " " + record.getAverageRating() + " " + record.getNumVotes());
            }
            return records;
        }

        public Boolean checkAvailableSpace()
        {
            if (getBlockSize() < this.blockSize)
                return true;
            else
                return false;
        }

        public int availableSpace()
        {
            return (this.blockSize - getBlockSize());
        }
        
        public void addNewRecord(Record record)
        { 
            records.Add(record);

            if (records.Count > 1)
            {
                // sort the records based on their numVotes in ascending order
                records.Sort((r1, r2) => r1.getTConst().CompareTo(r2.getTConst()));
            }
        }

        public int getNumberOfRecords()
        {
            return records.Count;
        }
    }
}