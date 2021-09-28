using System;
using System.Collections.Generic;

namespace Project_1
{
    class Start
    {
        public static void Main(string[] args)
        {
            // /*
            //  * For testing.tsv,
            //  * if block size = 100B, each block can hold 3 records, total number of blocks = 499/3 = 167
            //  * if block size = 500B, each block can hold 19 records, total number of blocks = 499/19 = 27
            //  */
            //Console.WriteLine("-----Database System Principle Project 1-----");
            //Console.WriteLine("Enter '1' to start storing data");
            //Console.WriteLine("Enter '2' run experiements");
            //Console.WriteLine("---------------------------------------------");
            //String userInput = Console.ReadLine();
            //if (userInput == "1")
            //{
            //    Program program = new Program();

            //    Console.WriteLine("---------------------------------------------");
            //    Console.WriteLine("Enter size of block:");
            //    Console.WriteLine("---------------------------------------------");
            //    string blockSizeInput = Console.ReadLine();
            //    Disk disk = program.start(int.Parse(blockSizeInput));

            //}
            //else if (userInput == "2")
            //{
            //    while (true)
            //    {
            //        Console.WriteLine("Enter which experiment to run: ");
            //        Console.WriteLine("Experiment 1: Storing data on the disk");
            //        Console.WriteLine("Experiment 2: Build a BPlus Tree based on 'numVotes'");
            //        Console.WriteLine("Experiment 3: Retrieve movies with 'numVotes' equal to 500");
            //        Console.WriteLine("Experiement 4: Delete movies of 'numVotes' equal to 1000");
            //        Console.WriteLine("5: Reset Program");
            //        string Input = Console.ReadLine();
            //        if (Input == "1")
            //        {
            //            //Exp 1: Report number of blocks and size of DB
            //            Console.WriteLine("Number of blocks: " + blocks);
            //            Console.WriteLine("Size of DB: " + size);
            //        }
            //        if (Input == "2")
            //        {
            //            //Exp 2: Sequential insertion and report n value, number of nodes height of the B+ tree and content of root & 1st child node
            //            Console.WriteLine("n value: ");
            //            Console.WriteLine("Number of nodes height: ");
            //            Console.WriteLine("Content of Root Node: ");
            //            Console.WriteLine("Content of 1st Child Node: ");
            //        }
            //        if (Input == "3")
            //        {
            //            //Exp 3: Report number and content of index nodes, number and content of data blocks the program accesses and avergae rating of records that are returned
            //            Console.WriteLine("Number of index nodes: ");
            //            Console.WriteLine("Content of index nodes: ");
            //            Console.WriteLine("Content of data blocks: ");
            //            Console.WriteLine("Average rating of of records: ");
            //        }
            //        if (Input == "4")
            //        {
            //            //Exp 4: Update the B + tree accordingly
            //            //Report the number of times that a node is deleted or two nodes are merged during the process of the updating the B + tree
            //            //Report the number nodes of the updated B + tree, " +
            //            //Report the height of the updated B + tree," +
            //            //Report the content of the root node and its 1st child node of the updated B + tree"
            //            Console.WriteLine("Number of node deletion: ");
            //            Console.WriteLine("Number of node mergers: ");
            //            Console.WriteLine("Height of updated B+ Tree: ");
            //            Console.WriteLine("Content of the Root Node: ");
            //            Console.WriteLine("Content of the 1st Child Node: ");
            //        }
            //        if (Input == "5")
            //        {
            //            continue;
            //        }
            //    }

            //  }
                Start s = new Start();
                s.test();

                //Program p = new Program();
                //p.test(); 
            }

        public void test()
        {
            List<LeafNode> l1 = new List<LeafNode>();
            Record r1 = new Record(new char[]{'a', 'b'}, 5.4, 1);
            Record r2 = new Record(new char[]{'a', 'b'}, 5.4, 4);
            Record r3 = new Record(new char[]{'a', 'b'}, 5.4, 7);
            Record r4 = new Record(new char[]{'a', 'b'}, 5.4, 10);
            Record r5 = new Record(new char[]{'a', 'b'}, 5.4, 5);
            Record r6 = new Record(new char[]{'a', 'b'}, 5.4, 6);
            Record r7 = new Record(new char[]{'a', 'b'}, 5.4, 3);
            Record r8 = new Record(new char[]{'a', 'b'}, 5.4, 13);
            //Record r9 = new Record(new char[]{'a', 'b'}, 5.4, 11);
            //Record r10 = new Record(new char[]{'a', 'b'}, 5.4, 14);
            // 1 4 5 | 7 10
            BPlusTree b = new BPlusTree();
            b.insert(r1.getNumVotes(), r1);
            b.insert(r2.getNumVotes(), r2);
            b.insert(r3.getNumVotes(), r3);
            b.insert(r4.getNumVotes(), r4);
            b.insert(r5.getNumVotes(), r5);
            b.insert(r6.getNumVotes(), r6);
            b.insert(r7.getNumVotes(), r7);
            b.insert(r8.getNumVotes(), r8);
            // b.insert(r9.getNumVotes(), r9);
            // b.insert(r10.getNumVotes(), r10);
        }
    }
}