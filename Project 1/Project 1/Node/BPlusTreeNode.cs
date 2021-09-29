using System.Collections.Generic;

namespace Project_1.Node
{
    public class BPlusTreeNode
    {
        private bool isLeaf;
        private List<int> keys;
        private BPlusTree pointer2next; //only for leaf nodes
        private pointer2TreeOrData _pointer2TreeOrData;

        public BPlusTreeNode(List<int> keys)
        {
            this.keys = keys;
        }

        public bool checkIsLeaf()
        {
            return isLeaf;
        }

        public void setIsLeaf(bool b)
        {
            this.isLeaf = b;
        }

        public int getKey(int index)
        {
            return keys[index];
        }

        public List<int> getAllKeys()
        {
            return keys;
        }

        public pointer2TreeOrData getPointer2TreeOrData()
        {
            return this._pointer2TreeOrData;
        }
    }

    public class pointer2TreeOrData
    {
        private List<BPlusTreeNode> pointer2InternalNodes;
        private List<Record> pointer2Records;

        public List<BPlusTreeNode> getPointer2InternalNodes()
        {
            return this.pointer2InternalNodes;
        }

        public List<Record> getPointer2Records()
        {
            return this.pointer2Records;
        }
    }

}