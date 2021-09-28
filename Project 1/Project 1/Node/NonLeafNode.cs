using System;
using System.Collections.Generic;

namespace Project_1
{
    public struct NonLeafNode
    {
        private List<int> keys;
        private List<NonLeafNode> nonLeafNodes;
        private List<LeafNode> leafNodes;
        private static int maxNumberOfKeys = 3;
        private int level;

        public NonLeafNode(int level)
        {
            keys = null;
            nonLeafNodes = null;
            leafNodes = null;
            this.level = level;
        }
        public int getMaxNumberOfKeys()
        {
            return maxNumberOfKeys;
        }

        public List<int> getKeys()
        {
            return keys;
        }
        
        public void setKeys(List<int> keys)
        {
            this.keys = keys;
        }

        public List<NonLeafNode> getNonLeafNodes()
        {
            return this.nonLeafNodes;
        }
        
        public void setNonLeafNodes(List<NonLeafNode> nonLeafNodes)
        {
            this.nonLeafNodes = nonLeafNodes;
        }
        
        public List<LeafNode> getLeafNodes()
        {
            return this.leafNodes;
        }
        
        public void setLeafNodes(List<LeafNode> leafNodes)
        {
            this.leafNodes = leafNodes;
        }
        
        public int getLevel()
        {
            return this.level;
        }
        
        public void setLevel(int level)
        {
            this.level = level;
        }

        // public void printAllKeys()
        // {
        //foreach (int i in keys)
        // {
        // Console.WriteLine("key = " + i);
        //}
        // }
    }
}