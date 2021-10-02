using System;
using System.Collections.Generic;
using System.IO;

namespace Project_1.Data_Exploration
{
    public class Data
    {
        public void dataAnalysis()
        {
            int highestCount = 0;
            string tconst = "";
            int largestNumVotes = 0;
            using (var reader = new StreamReader("C:\\Users\\jingh\\Desktop\\CE-CZ4031-Projects\\Project 1\\Project 1\\data.tsv"))
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
                        if (values[0].Length > highestCount)
                        {
                            highestCount = values[0].Length;
                            tconst = values[0];
                        }

                        if (int.Parse(values[2]) > largestNumVotes)
                            largestNumVotes = int.Parse(values[2]);
                    }
                }
            }
            Console.WriteLine("max number of characters for tconst = {0}", highestCount);
            Console.WriteLine("tconst = {0}", tconst);
            Console.WriteLine("largest numVotes = {0}", largestNumVotes);
        }
    }
}