using System;
using Microsoft.VisualBasic;

namespace Project_1
{
    public struct Record
    {
        private char[] tConst;
        private double averageRating;
        private int numVotes;
        private int blockID;
        
        public Record(char[] tConst, double averageRating, int numVotes)
        {
            this.tConst = tConst;
            this.averageRating = averageRating;
            this.numVotes = numVotes;
            this.blockID = 0;
        }

        public char[] getTConst()
        {
            return this.tConst;
        }

        public double getAverageRating()
        {
            return this.averageRating;
        }

        public int getNumVotes()
        {
            return this.numVotes;
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
            return sizeof(char) * 10 + sizeof(double) + 2*sizeof(int); //2*10 + 8 + 4 + 4= 36
        }

        public void printRecord()
        {
            string str = new string(getTConst());
            Console.WriteLine("tconst = " + str + ", averageRating = " + getAverageRating()
            + ", numVotes = " + getNumVotes() + " blockID = " + blockID);
            
        }
    }
}
