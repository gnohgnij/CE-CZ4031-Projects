using System;
using System.Collections.Generic;

namespace Project_1
{
    class Start
    {
        public static void Main(string[] args)
        {
            /*
             * For testing.tsv,
             * if block size = 100B, each block can hold 3 records, total number of blocks = 499/3 = 167
             * if block size = 500B, each block can hold 19 records, total number of blocks = 499/19 = 27
             */
            // Console.WriteLine("-----Database System Principle Project 1-----");
            // Console.WriteLine("Enter '1' to start storing data");
            // Console.WriteLine("Enter '2' run experiements");
            // String userInput = Console.ReadLine();
            // if (userInput == "1")
            // {
            //     Program program = new Program();
            //     Console.WriteLine("Enter size of block:");
            //     string blockSizeInput = Console.ReadLine();
            //     Disk disk = program.start(int.Parse(blockSizeInput));
            //
            //     //Program p = new Program();
            //     //p.test();
            // }
            // else if(userInput == "2")
            // {
            //     while (true)
            //     {
            //         Console.WriteLine("Enter which experiment to run: ");
            //         Console.WriteLine("Experiment 1: Storing data and report number of blocks and size of DB");
            //         Console.WriteLine("Experiment 2: Build a BPlus Tree based on 'numVotes' by sequential insertion and report n value, number of nodes," +
            //             " height of the B+ tree and content of root & 1st child node");
            //         Console.WriteLine("Experiment 3: Retrieve movies with 'numVotes' equal to 500 and report " +
            //             "number and content of index nodes, number and content of data blocks the program accesses and " +
            //             "avergae rating of records that are returned.");
            //         Console.WriteLine("Experiement 4: Delete movies of ï¿½numVotesï¿½ equal to 1000, update the B + tree accordingly, and report " +
            //             "the number of times that a node is deleted or two nodes are merged during the process of the updating the B + tree, " +
            //             "the number nodes of the updated B + tree, " +
            //             "the height of the updated B + tree," +
            //             " the content of the root node and its 1st child node of the updated B + tree");
            //         Console.WriteLine("5: Reset Program");
            //         string Input = Console.ReadLine();
            //         if (Input == "1")
            //         {
            //             //Exp 1
            //         }
            //         if (Input == "2")
            //         {
            //             //Exp 2
            //         }
            //         if (Input == "3")
            //         {
            //             //Exp 3
            //         }
            //         if (Input == "4")
            //         {
            //             //Exp 4
            //         }
            //         if (Input == "5")
            //         {
            //             continue;
            //         }
            //     }
            //
            // }
            Start s = new Start();
            s.test();

<<<<<<< HEAD
                //Program p = new Program();
                //p.test();
            }
            else if(userInput == "2")
            {
                while (true)
                {
                    Console.WriteLine("Enter which experiment to run: ");
                    Console.WriteLine("Experiment 1: Storing data and report number of blocks and size of DB");
                    Console.WriteLine("Experiment 2: Build a BPlus Tree based on 'numVotes' by sequential insertion and report n value, number of nodes," +
                        " height of the B+ tree and content of root & 1st child node");
                    Console.WriteLine("Experiment 3: Retrieve movies with 'numVotes' equal to 500 and report " +
                        "number and content of index nodes, number and content of data blocks the program accesses and " +
                        "avergae rating of records that are returned.");
                    Console.WriteLine("Experiement 4: Delete movies of “numVotes” equal to 1000, update the B + tree accordingly, and report " +
                        "the number of times that a node is deleted or two nodes are merged during the process of the updating the B + tree, " +
                        "the number nodes of the updated B + tree, " +
                        "the height of the updated B + tree," +
                        " the content of the root node and its 1st child node of the updated B + tree");
                    Console.WriteLine("5: Reset Program");
                    Console.WriteLine("6: Quit Program");
                    string Input = Console.ReadLine();
                    if (Input == "1")
                    {
                        //Exp 1
                        continue;
                    }
                    if (Input == "2")
                    {
                        //Exp 2
                        continue;
                        
                    }
                    if (Input == "3")
                    {
                        //Exp 3
                        continue;
                    }
                    if (Input == "4")
                    {
                        //Exp 4
                        continue;
                    }
                    if (Input == "5")
                    {
                        continue;
                    }
                    if( Input == "6")
                    {
                        break;
                    }
                }
=======
        }
        
        public void test()
        {
            List<LeafNode> l1 = new List<LeafNode>();
            Record r1 = new Record(new char[]{'a', 'b'}, 5.4, 0);
            Record r2 = new Record(new char[]{'a', 'b'}, 5.4, 1);
            Record r3 = new Record(new char[]{'a', 'b'}, 5.4, 2);
            Record r4 = new Record(new char[]{'a', 'b'}, 5.4, 3);
>>>>>>> 78ff0e76fb249c5b9d800d3bdb4cb54b8e01d7de

            BPlusTree b = new BPlusTree();
            b.insert(r1.getNumVotes(), r1);
            b.insert(r2.getNumVotes(), r2);
            b.insert(r3.getNumVotes(), r3);
            b.insert(r4.getNumVotes(), r4);
        }
    }
}