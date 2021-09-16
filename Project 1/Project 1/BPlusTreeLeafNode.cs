using System;

namespace Project_1
{
    public class BPlusTreeLeafNode<Tkey> : BPlusTreeNode<Tkey> where Tkey : IComparable<Tkey>
    {
        /*
         * leaf nodes have:
         *      1. keys - min = floor (n+1)/2, max = n
         *      2. values/pointers - keys + 1
         *      3. parent node - 1 per leaf node
         *      4. left sibling (except for left-most leaf node)
         *      5. right sibling (except for right-most leaf node)
         */
        
        private int n;
        private object[] values;

        public BPlusTreeLeafNode()
        {
            this.keys = new object[n];
            this.values = new object[n + 1];
        }

        public override NodeType getNodeType()
        {
            return NodeType.LeafNode;
        }
    }
}