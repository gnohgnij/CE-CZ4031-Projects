

using System;
using System.Collections.Generic;

namespace Project_1
{
    public struct Insert
    {
        private List<int> keys;
        private List<Record> pointers;

        private int index;
        private int pointing;

        public InsertRecord()
        {
            keys = null;
            pointers = null;
            LeafNode node = new LeafNode(keys, pointers);
            if (node.getMaxNumberOfKeys() > 3) 
            {
                // floor function for floorfunction(n+1) + 1 

            }

        }

        public List<LeafNode> leftNode()
        {
            List<LeafNode> leftNode = new List<LeafNode>();
            return leftNode;
        }

        public List<LeafNode> rightNode()
        {
            List<LeafNode> rightNode = new List<LeafNode>();
            return rightNode;
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

        public bool isLeaf()
        {
            return false;
        }
    }
}