using System;

namespace Project_1
{
    public struct Record
    {
        private string tconst;
        private double averageRating;
        private int numVotes;
        
        public Record(string tconst, double averageRating, int numVotes)
        {
            this.tconst = tconst;
            this.averageRating = averageRating;
            this.numVotes = numVotes;
        }

        public string getTConst()
        {
            return this.tconst;
        }

        public double getAverageRating()
        {
            return this.averageRating;
        }

        public int getNumVotes()
        {
            return this.numVotes;
        }
    }
}