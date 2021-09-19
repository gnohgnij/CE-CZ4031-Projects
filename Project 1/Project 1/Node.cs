using System;
using System.Collections.Generic;

namespace Project_1
{
    public struct Node
    {
        private LinkedList<LeafNode> leafList;
        static int maxNode = 3; // change when we confirm maxNode

        public Node(LinkedList<LeafNode> leafList) 
        {
            this.leafList = leafList;
        }

        public void addLeafNode(Record record) 
        {
            leafList.AddLast(new LeafNode(record.getNumVotes(), 10)); // get pointer from record
            // easy cause inserting in sequence
        }

        public void printLeafNode() 
        {
            foreach (var leafNode in leafList) 
            {
                Console.WriteLine(leafNode.getIndex());
            }
        }
    }
}