using System;
using Microsoft.VisualBasic;

namespace Project_1
{
    public struct Record
    {
        private char[] tConst;
        private double averageRating;
        private int numVotes;
        
        public Record(char[] tConst, double averageRating, int numVotes)
        {
            this.tConst = tConst;
            this.averageRating = averageRating;
            this.numVotes = numVotes;
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

        public int getBytes()
        {
            return sizeof(char) * 7 + sizeof(double) + sizeof(int); //2*7 + 8 + 4 = 26
        }

        public void printRecord()
        {
            string str = new string(getTConst());
            Console.WriteLine("tconst = " + str + ", averageRating = " + getAverageRating()
            + ", numVotes = " + getNumVotes());
        }
    }
}
