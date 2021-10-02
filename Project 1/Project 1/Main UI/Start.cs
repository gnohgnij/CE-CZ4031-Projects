using System;
using System.Collections.Generic;
using Project_1;
using Project_1.Node;

namespace Project_1
{
    class Start
    {
        public static void Main(string[] args)
        {

            // Disk disk;
            // Program program;
            //
            // Console.WriteLine("-----Database System Principle Project 1-----");
            // Console.WriteLine("Enter any key to start storing data");
            // Console.WriteLine("---------------------------------------------");
            // string input = Console.ReadLine();
            // if(string.IsNullOrEmpty(input))
            //     Console.WriteLine("No keys entered, please try again...");
            // else
            // {
            //     Console.WriteLine("Enter size of block (100 or 500): ");
            //     string blockSizeInput = Console.ReadLine();
            //     Console.WriteLine("---------------------------------------------");
            //     int blockSize = int.Parse(blockSizeInput);
            //     if(!(blockSize == 100 || blockSize == 500))
            //         Console.WriteLine("Invalid block size entered, please try again...");
            //     else
            //     {
            //         Console.WriteLine("Block size {0} bytes entered, storing data now...", blockSize);
            //         program = new Program();
            //         disk = program.start(blockSize);
            //         Console.WriteLine("Total size of database in MB = {0}", disk.getNumberOfBlocks()*blockSize/Math.Pow(10, 6));
            //         List<Block> listOfBlocks = disk.getBlocks();
            //         BPTree bpTree = new BPTree();
            //         bpTree.setMaxLeafNodeLimit(blockSize);
            //         bpTree.setMaxChildLimit(blockSize);
            //         for (int i = 0; i < listOfBlocks.Count; i++)
            //         {
            //             List<Record> listOfRecords = listOfBlocks[i].getRecords();
            //             for (int j = 0; j < listOfRecords.Count; j++)
            //             {
            //                 bpTree.insert(listOfRecords[j].getNumVotes(), listOfRecords[j]);
            //             }
            //         }
            //         Console.WriteLine("Data storing completed!");
            //         Console.WriteLine("---------------------------------------------");
            //     }
            // }
            
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

            // Program p = new Program();
            // Disk d = p.start(100);
        }

        public void test()
        {
            Record r1 = new Record(new char[]{'a', 'b'}, 5.4, 4);
            Record r2 = new Record(new char[]{'a', 'b'}, 5.4, 1);
            Record r3 = new Record(new char[]{'a', 'b'}, 5.4, 7);
            Record r4 = new Record(new char[]{'a', 'b'}, 5.4, 10);
            Record r5 = new Record(new char[]{'a', 'b'}, 9.9, 17);
            Record r6 = new Record(new char[]{'a', 'b'}, 5.3, 21);
            Record r7 = new Record(new char[]{'a', 'b'}, 6.3, 31);
            Record r8 = new Record(new char[]{'a', 'b'}, 5.4, 25);
            Record r9 = new Record(new char[]{'a', 'b'}, 5.4, 19);
            Record r10 = new Record(new char[]{'a', 'b'}, 5.4, 20);
            Record r11 = new Record(new char[]{'a', 'b'}, 2.3, 28);
            Record r12 = new Record(new char[]{'a', 'b'}, 5.4, 42);
            Record r13 = new Record(new char[]{'a', 'b'}, 5.4, 5);
            Record r14 = new Record(new char[]{'a', 'b'}, 5.4, 6);
            Record r15 = new Record(new char[]{'a', 'b'}, 5.4, 2);
            Record r16 = new Record(new char[]{'a', 'b'}, 5.4, 3);
            Record r17 = new Record(new char[]{'a', 'b'}, 5.4, 32);
            Record r18 = new Record(new char[]{'a', 'b'}, 5.4, 33);
            Record r19 = new Record(new char[]{'a', 'b'}, 8.8, 21);
            Record r20 = new Record(new char[]{'a', 'b'}, 9.9, 21);
            Record r21 = new Record(new char[]{'a', 'b'}, 5.4, 29);
            Record r22 = new Record(new char[]{'a', 'b'}, 5.4, 26);
            Record r23 = new Record(new char[]{'a', 'b'}, 5.4, 27);
            Record r24 = new Record(new char[]{'a', 'b'}, 5.4, 24);
            Record r25 = new Record(new char[]{'a', 'b'}, 5.4, 22);

            Record r26 = new Record(new char[] { 'a', 'b' }, 5.4, 41);
            Record r27 = new Record(new char[] { 'a', 'b' }, 5.4, 46);
            Record r28 = new Record(new char[] { 'a', 'b' }, 5.4, 88);
            Record r29 = new Record(new char[] { 'a', 'b' }, 5.4, 0);
            Record r30 = new Record(new char[] { 'a', 'b' }, 5.4, 22);

            Record r31 = new Record(new char[] { 'a', 'b' }, 5.4, 141);
            Record r32 = new Record(new char[] { 'a', 'b' }, 5.4, 146);
            Record r33 = new Record(new char[] { 'a', 'b' }, 5.4, 188);
            Record r34 = new Record(new char[] { 'a', 'b' }, 5.4, 10);
            Record r35 = new Record(new char[] { 'a', 'b' }, 5.4, 122);

            Record r36 = new Record(new char[] { 'a', 'b' }, 5.4, 241);
            Record r37 = new Record(new char[] { 'a', 'b' }, 5.4, 246);
            Record r38 = new Record(new char[] { 'a', 'b' }, 5.4, 288);
            Record r39 = new Record(new char[] { 'a', 'b' }, 5.4, 200);
            Record r40 = new Record(new char[] { 'a', 'b' }, 5.4, 222);

            Record r41 = new Record(new char[] { 'a', 'b' }, 5.4, 341);
            Record r42 = new Record(new char[] { 'a', 'b' }, 5.4, 346);
            Record r43 = new Record(new char[] { 'a', 'b' }, 5.4, 388);
            Record r44 = new Record(new char[] { 'a', 'b' }, 5.4, 30);
            Record r45 = new Record(new char[] { 'a', 'b' }, 5.4, 322);

            Record r46 = new Record(new char[] { 'a', 'b' }, 5.4, 341);
            Record r47 = new Record(new char[] { 'a', 'b' }, 5.4, 346);
            Record r48 = new Record(new char[] { 'a', 'b' }, 5.4, 388);
            Record r49 = new Record(new char[] { 'a', 'b' }, 5.4, 310);
            Record r50 = new Record(new char[] { 'a', 'b' }, 5.4, 322);

            Record r51 = new Record(new char[] { 'a', 'b' }, 5.4, 541);
            Record r52 = new Record(new char[] { 'a', 'b' }, 5.4, 546);
            Record r53 = new Record(new char[] { 'a', 'b' }, 5.4, 588);
            Record r54 = new Record(new char[] { 'a', 'b' }, 5.4, 500);
            Record r55 = new Record(new char[] { 'a', 'b' }, 5.4, 522);



            BPTree b = new BPTree();
            b.setMaxChildLimit(44);
            b.setMaxLeafNodeLimit(44);
            b.insert(r1.getNumVotes(), r1);
            b.insert(r2.getNumVotes(), r2);
            b.insert(r3.getNumVotes(), r3);
            b.insert(r4.getNumVotes(), r4);
            b.insert(r5.getNumVotes(), r5);
            b.insert(r6.getNumVotes(), r6);
            b.insert(r7.getNumVotes(), r7);
            b.insert(r8.getNumVotes(), r8);
            b.insert(r9.getNumVotes(), r9);
            b.insert(r10.getNumVotes(), r10);
            b.insert(r11.getNumVotes(), r11);
            b.insert(r12.getNumVotes(), r12);
            b.insert(r13.getNumVotes(), r13);
            b.insert(r14.getNumVotes(), r14);
            b.insert(r15.getNumVotes(), r15);
            b.insert(r16.getNumVotes(), r16);
            b.insert(r17.getNumVotes(), r17);
            b.insert(r18.getNumVotes(), r18);
            b.insert(r19.getNumVotes(), r19);
            b.insert(r20.getNumVotes(), r20);
            b.insert(r21.getNumVotes(), r21);
            b.insert(r22.getNumVotes(), r22);
            b.insert(r23.getNumVotes(), r23);
            b.insert(r24.getNumVotes(), r24);

            b.insert(r25.getNumVotes(), r25);

            b.insert(r26.getNumVotes(), r26);
            b.insert(r27.getNumVotes(), r27);
            b.insert(r28.getNumVotes(), r28);
            b.insert(r29.getNumVotes(), r29);
            b.insert(r30.getNumVotes(), r30);

            b.insert(r31.getNumVotes(), r31);
            b.insert(r32.getNumVotes(), r32);
            b.insert(r33.getNumVotes(), r33);
            b.insert(r34.getNumVotes(), r34);
            b.insert(r35.getNumVotes(), r35);

            b.insert(r36.getNumVotes(), r36);
            b.insert(r37.getNumVotes(), r37);
            //b.insert(r38.getNumVotes(), r38);
            //b.insert(r39.getNumVotes(), r39);
            //b.insert(r40.getNumVotes(), r40);

            //b.insert(r41.getNumVotes(), r41);
            //b.insert(r42.getNumVotes(), r42);
            //b.insert(r43.getNumVotes(), r43);
            //b.insert(r44.getNumVotes(), r44);
            //b.insert(r45.getNumVotes(), r45);

            //b.insert(r46.getNumVotes(), r46);
            //b.insert(r47.getNumVotes(), r47);
            //b.insert(r48.getNumVotes(), r48);
            //b.insert(r49.getNumVotes(), r49);
            //b.insert(r50.getNumVotes(), r50);

            //b.insert(r51.getNumVotes(), r51);
            //b.insert(r52.getNumVotes(), r52);
            //b.insert(r53.getNumVotes(), r53);
            //b.insert(r54.getNumVotes(), r54);
            //b.insert(r55.getNumVotes(), r55);
            BPlusTreeNode pter = b.getRoot();
            b.printTree(pter);
            b.setMaxChildLimit(44);
            b.setMaxLeafNodeLimit(44);
            b.totalNodes(pter);
            // b.getRootContent();
            // b.searchRange(21, 21);
            bool lanjiao = b.search(21); 
            while(lanjiao)
            {
                b.delete(21);
                lanjiao = b.search(21);
                b.totalNodes(pter);
                b.printTree(pter);
            }
            b.totalNodes(pter);
            // b.delete(21);
            // b.searchRange(17,41);
            // b.delete(21);
            // b.printTree(pter);
            // b.delete(1000);
            // b.printTree(pter);
            // b.delete(2000);
            // b.printTree(pter);
        }
    }
}