using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Project_1
{
    public struct BPlusTree
    {
        private List<LeafNode> leafNodes;

        public BPlusTree(List<LeafNode> leafNodes)
        {
            this.leafNodes = leafNodes;
        }

        public void insert(int numVotes, Record record)
        {
            if (leafNodes.Count == 1)
            {
                // leafNodes.Add(new LeafNode(new List<int>(), new List<Record>()));
                leafNodes[0].getKeys().Add(numVotes);
                leafNodes[0].getPointers().Add(record);
                // Console.Write("LeafNode[0] = ");
                // leafNodes[0].printAllKeys();
                
                Console.WriteLine(leafNodes[0].getKeys().Count);
            }
            
            if (leafNodes[0].getKeys().Count >= leafNodes[0].getMaxNumberOfKeys())
            {
                Console.WriteLine("hello");
                List<int> temp = leafNodes[0].getKeys();
                temp.Add(numVotes);
                temp.Sort((k1, k2) => k1.CompareTo(k2));
                leafNodes.Add(new LeafNode(new List<int>(), new List<Record>()));

                leafNodes[0].getKeys().Add(999);
                // for (int i = 0; i < leafNodes[0].getMaxNumberOfKeys()/2; i++)
                // {
                //     // leafNodes[0].getKeys().Add(temp[i]);
                // }
                // for (int i = leafNodes[0].getMaxNumberOfKeys()/2; i < leafNodes[0].getMaxNumberOfKeys(); i++)
                // {
                //     leafNodes[1].getKeys().Add(temp[i]);
                // }

                Console.Write("LeafNode[0] = ");
                leafNodes[0].printAllKeys();
                Console.Write("LeafNode[1] = ");
                leafNodes[1].printAllKeys();
            }
        }
    }
}