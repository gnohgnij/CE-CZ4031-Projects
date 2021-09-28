using System.Collections.Generic;

namespace Project_1
{
    public struct NonLeafNode
    {
        // private int index;
        //   private int pointing;
        private List<Record> pointers;
        private List<int> keys;
        private static int maxNumberOfKeys = 3;

        public NonLeafNode(List<int> keys, List<Record> pointers)
        {
            this.keys = keys;
            this.pointers = pointers;
        }
        public int getMaxNumberOfKeys()
        {
            return maxNumberOfKeys;
        }
        public bool isLeaf()
        {
            return false;
        }
    }
}