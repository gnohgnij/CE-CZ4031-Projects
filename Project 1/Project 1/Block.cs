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
            return this.blockSize;
        }

        public void setBlockSize(int blockSize)
        {
            this.blockSize = blockSize;
        }
        
        public List<Record> getRecords()
        {
            return this.records;
        }

        // public Boolean checkAvailableSpace()
        // {
        //     if (this.blockSize > Marshal.SizeOf(this.records))
        //         return true;
        //     else
        //         return false;
        // }
        
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