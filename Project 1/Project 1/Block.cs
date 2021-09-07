using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Project_1
{
    public struct Block
    {
        private List<Record> records;

        public Block(List<Record> records)
        {
            this.records = records;

            if (records.Count > 1)
            {
                // sort the records based on their numVotes in ascending order
                records.Sort((r1, r2) => r1.getNumVotes().CompareTo(r2.getNumVotes()));    
            }
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
            if (getBlockSize() < 100)
                return true;
            else
                return false;
        }

        public int availableSpace()
        {
            return (100 - getBlockSize());
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