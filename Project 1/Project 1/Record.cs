using System;

namespace Project_1
{
    struct Record
    {
        private string tconst;
        private int averageRating;
        private int numVotes;
        
        public Record(string tconst, int averageRating, int numVotes)
        {
            this.tconst = tconst;
            this.averageRating = averageRating;
            this.numVotes = numVotes;
        }

        public string getTConst()
        {
            return this.tconst;
        }

        public int getAverageRating()
        {
            return this.averageRating;
        }

        public int getNumVotes()
        {
            return this.numVotes;
        }
    }
}