using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_1
{
    public struct BPlusTree
    {
        private LinkedList<LeafNode> leafList;
        static int maxKey = 3; // change when we confirm maxNode

        public BPlusTree(LinkedList<LeafNode> leafList) 
        {
            this.leafList = leafList;
        }

        public void addLeafNode(Record record) 
        {
            //leafList.AddLast(new LeafNode(record.getNumVotes())); // get pointer from record
            // easy cause inserting in sequence
        }

        public void printLeafNode() 
        {
            foreach (var leafNode in leafList) 
            {
                //Console.WriteLine(leafNode.getIndex());
            }
        }

        public int floorFunctionLeafNode(float value)
        {
            int floor = (int)Math.Floor((value + 1) / 2);
            return floor;
        }

        public int floorFunctionNonLeafNode(float value)
        {
            int floor = (int)Math.Floor(value / 2);
            return floor;
        }

        public int getIndex(int key) //get key from linked list
        {
            return 1;
        }

        public void sortValue() 
        {

        }

        public int getSize() 
        {

            return leafList.Count();
        }
    }
}