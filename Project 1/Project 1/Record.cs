using System;

namespace Project_1
{
    public struct Record
    {
        private string tConst;
        private double averageRating;
        private int numVotes;
        
        public Record(string tConst, double averageRating, int numVotes)
        {
            this.tConst = tConst;
            this.averageRating = averageRating;
            this.numVotes = numVotes;
        }

        public string getTConst()
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
            return (
                System.Text.ASCIIEncoding.ASCII.GetByteCount(this.tConst) + 
                System.Text.ASCIIEncoding.ASCII.GetByteCount(this.averageRating.ToString()) +
                System.Text.ASCIIEncoding.ASCII.GetByteCount(this.numVotes.ToString())
            );
        }
    }
}