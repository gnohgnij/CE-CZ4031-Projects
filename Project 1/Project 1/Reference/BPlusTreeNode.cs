using System;

namespace Project_1
{
    public enum NodeType
    {
        LeafNode,
        InternalNode
    }
    
    public abstract class BPlusTreeNode<TKey> where TKey : IComparable<TKey>
    {
        protected object[] keys;
        protected int keyCount;
        protected BPlusTreeNode<TKey> parentNode;
        protected BPlusTreeNode<TKey> leftSibling;
        protected BPlusTreeNode<TKey> rightSibling;

        public BPlusTreeNode()
        {
            this.keyCount = 0;
            this.leftSibling = null;
            this.rightSibling = null;
            this.parentNode = null;
        }

        public int getKeyCount()
        {
            return this.keyCount;
        }

        public TKey getKey(int index)
        {
            return (TKey) this.keys[index];
        }

        public void setKey(int index, TKey key)
        {
            this.keys[index] = key;
        }

        public BPlusTreeNode<TKey> getParent()
        {
            return this.parentNode;
        }
        
        public BPlusTreeNode<TKey> getLeftSibling()
        {
            if (this.leftSibling != null && this.getParent() == this.leftSibling.getParent())
                return leftSibling;
            return null;
        }
        
        public BPlusTreeNode<TKey> getRightSibling()
        {
            if (this.rightSibling != null && this.getParent() == this.rightSibling.getParent())
                return rightSibling;
            return null;
        }

        public void setParent(BPlusTreeNode<TKey> parentNode)
        {
            this.parentNode = parentNode;
        }

        public void setRightSibling(BPlusTreeNode<TKey> rightSibling)
        {
            this.rightSibling = rightSibling;
        }
        
        public void setLeftSibling(BPlusTreeNode<TKey> leftSibling)
        {
            this.leftSibling = leftSibling;
        }

        public abstract NodeType getNodeType();
        
        /*
         * search(int numVotes):
         *      need to store no. of index nodes accessed
         *      need to store contents of index nodes accessed (first 5 if > 5)
         *      need to store no. of data blocks accessed
         *      need to store contents of data blocks accessed (first 5 if > 5)
         *      need to calculate the average of the averageRating
         */
    }
}