using System;
using System.Collections.Generic;

namespace Project_1.Node
{
    public class BPlusTreeNode
    {
        private bool isLeaf;
        private List<int> keys;
        private BPlusTreeNode pointer2next; //only for leaf nodes
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

        public BPlusTreeNode getPointer2Next()
        {
            return this.pointer2next;
        }

        public void setPointer2Next(BPlusTreeNode newNode)
        {
            this.pointer2next = newNode;
        }

        public pointer2TreeOrData getPointer2TreeOrData(List<BPlusTreeNode> pointer2InternalNodes, 
            List<Record> pointer2Records)
        {
            if(this._pointer2TreeOrData != null)
                return this._pointer2TreeOrData;
            this._pointer2TreeOrData = new pointer2TreeOrData(pointer2InternalNodes, pointer2Records);
            return _pointer2TreeOrData;
        }
    }

    public class pointer2TreeOrData
    {
        private List<BPlusTreeNode> pointer2InternalNodes;
        private List<Record> pointer2Records;

        public pointer2TreeOrData(List<BPlusTreeNode> pointer2InternalNodes, List<Record> pointer2Records)
        {
            this.pointer2InternalNodes = pointer2InternalNodes;
            this.pointer2Records = pointer2Records;
        }

        public List<BPlusTreeNode> getPointer2InternalNodes()
        {
            return this.pointer2InternalNodes;
        }

        public List<Record> getPointer2Records()
        {
            return this.pointer2Records;
        }

        public void printAllRecords()
        {
            foreach (Record r in pointer2Records)
            {
                r.printRecord();
            }
            Console.WriteLine("-----------------");
        }
    }

}