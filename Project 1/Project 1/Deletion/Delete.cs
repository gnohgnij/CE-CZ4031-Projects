

using System;
using System.Collections.Generic;

namespace Project_1
{
    public struct Delete
    {
        private List<int> keys;
        private List<Record> pointers;

        //private int index;
        // private int pointing;

        public Delete(List<int> keys, List<Record> pointers)
        {
            this.keys = null;
            this.pointers = null;
            LeafNode leafNode = new LeafNode(keys, pointers);

            if (leafNode.getMaxNumberOfKeys() > 3)
            {
                //leftNode();
                //leftNode = floorFunctionLeafNode(keys.Count);
            }
            NonLeafNode nonLeafNode = new NonLeafNode(keys, pointers);
            if (nonLeafNode.getMaxNumberOfKeys() > 3)
            {
                // rightNode = floorFunctionNonLeafNode(keys.Count);
            }
        }
    }
}