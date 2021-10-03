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
            Start start = new Start();
            Disk disk;
            Program program;
            BPTree bpTree;
            
            Console.WriteLine("-----Database System Principle Project 1-----");
            Console.WriteLine("Enter any key to start storing data");
            Console.WriteLine("---------------------------------------------");
            string input = Console.ReadLine();
            if(string.IsNullOrEmpty(input))
                Console.WriteLine("No keys entered, please try again...");
            else
            {
                Console.WriteLine("Enter size of block (100 or 500): ");
                string blockSizeInput = Console.ReadLine();
                Console.WriteLine("---------------------------------------------");
                int blockSize = int.Parse(blockSizeInput);
                if(!(blockSize == 100 || blockSize == 500))
                    Console.WriteLine("Invalid block size entered, please try again...");
                else
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Experiment 1 starting...");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("Block size {0} bytes entered, storing data now...", blockSize);
                    program = new Program();
                    disk = program.start(blockSize);
                    Console.WriteLine("Total size of database in MB = {0}", disk.getNumberOfBlocks()*blockSize/Math.Pow(10, 6));
                    bpTree = start.Experiment2(blockSize, disk);
                    start.Experiment3(bpTree, disk);
                    start.Experiment4(bpTree, disk);
                    start.Experiment5(bpTree);
                }
            }
        }


        public BPTree Experiment2(int blockSize, Disk disk)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Experiment 2 starting...");
            Console.WriteLine("---------------------------------------------");
            BPTree bpTree = new BPTree();
            BPlusTreeNode pointer = new BPlusTreeNode(new List<int>());
            List<Block> listOfBlocks = disk.getBlocks();
            //Exp 2: Sequential insertion and report n value, number of nodes height of the B+ tree and content of root & 1st child node
            Console.WriteLine("Creating B+ Tree...");
            bpTree.setMaxLeafNodeLimit(blockSize);
            bpTree.setMaxChildLimit(blockSize);
            for (int i = 0; i < listOfBlocks.Count; i++)
            {
                List<Record> listOfRecords = listOfBlocks[i].getRecords();
                for (int j = 0; j < listOfRecords.Count; j++)
                {
                    bpTree.insert(listOfRecords[j].getNumVotes(), listOfRecords[j]);
                }
            }

         
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("n value: " + bpTree.getMaxLeafNodeLimit());
            pointer = bpTree.getRoot();
            bpTree.totalNodes(pointer);
            bpTree.getRootContent();
            Console.WriteLine("---------------------------------------------");

            return bpTree;
        }
        public void Experiment3(BPTree bpTree, Disk disk)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Experiment 3 starting...");
            Console.WriteLine("---------------------------------------------");
            bpTree.searchRange(500, 500, disk);

        }
        public void Experiment4(BPTree bpTree, Disk disk)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Experiment 4 starting...");
            Console.WriteLine("---------------------------------------------");
            bpTree.searchRange(30000, 40000, disk);
        }
        public void Experiment5(BPTree bpTree)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Experiment 5 starting...");
            Console.WriteLine("---------------------------------------------");
            
            BPlusTreeNode pointer = new BPlusTreeNode(new List<int>());
            pointer = bpTree.getRoot();
            int deleteKey = 1000;
            bpTree.setDeleteNode();

            bool search = bpTree.delete(deleteKey);
            while (search)
            {
                search = bpTree.delete(deleteKey);
            }
            Console.WriteLine("Nodes deleted is: " + bpTree.getDeleteNode());
            bpTree.totalNodes(pointer);
            bpTree.getRootContent();
            //bpTree.printTree(pointer);
        }
    }
}