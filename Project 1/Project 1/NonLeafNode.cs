using System.Collections.Generic;

namespace Project_1
{
    public struct NonLeafNode
    {
        // private int index;
        //   private int pointing;
        private List<Record> pointers;
        private List<int> keys;

        public NonLeafNode(List<Record> pointers, List<int> keys)
        {
            this.pointers = pointers;
            this.keys = keys;
        }

        public bool isLeaf()
        {
            return false;
        }
    }
}