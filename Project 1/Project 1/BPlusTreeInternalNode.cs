using System;
using System.IO.Enumeration;

namespace Project_1
{
    public class BPlusTreeInternalNode<TKey> : BPlusTreeNode<TKey> where TKey : IComparable<TKey>
    {
        /*
         * internal nodes have:
         *      1. keys - min = floor n/2, max = n (root - min = 0, max = n)
         *      2. children nodes/pointers - keys + 1
         *      3. parent node - 1 parent per internal node
         */
        
        private int n;
        private object[] children;
        
        public BPlusTreeInternalNode()
        {
            this.keys = new object[n];
            this.children = new object[n + 1];
        }
        
        public BPlusTreeNode<TKey> getChild(int index)
        {
            // return BPlusTreeNode because don't know whether it is a leaf/internal node
            return (BPlusTreeNode<TKey>) this.children[index];
        }

        public void setChild(int index, BPlusTreeNode<TKey> childNode)
        {
            this.children[index] = childNode;
            if(childNode != null)
                childNode.setParent(this);
        }

        public override NodeType getNodeType()
        {
            return NodeType.InternalNode;
        }
    }
}