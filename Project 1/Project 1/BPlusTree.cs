using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Project_1
{
    public struct BPlusTree
    {
        private List<LeafNode> leafNodes;

        public void insert(int numVotes, Record record)
        {
            if (leafNodes == null)  //when BPlusTree is empty
            {
                Console.WriteLine("leafNodes == null");
                this.leafNodes = new List<LeafNode>();
                leafNodes.Add(new LeafNode(new List<int>(), new List<Record>()));
                leafNodes[0].getKeys().Add(numVotes);
                leafNodes[0].getPointers().Add(record);
                Console.WriteLine("LeafNodes[0]: ");
                leafNodes[0].printAllKeys();
                Console.WriteLine("--------------");
            }

            else
            {
                int currIndex = this.leafNodes.Count - 1;   //need to change this when searching for which leafnode to insert to
                if (leafNodes[currIndex].getKeys().Count == leafNodes[0].getMaxNumberOfKeys())  //when current leaf node is at max capacity
                {
                    List<int> keyArr = new List<int>();
                    foreach (int i in leafNodes[currIndex].getKeys())
                    {
                        keyArr.Add(i);
                    }
                    leafNodes[currIndex].getKeys().Clear();
                    
                    keyArr.Add(numVotes);
                    
                    keyArr.Sort((k1, k2) => k1.CompareTo(k2));
                    
                    for (int i = 0; i < (leafNodes[currIndex].getMaxNumberOfKeys() / 2) + 1; i++)
                    {
                        leafNodes[currIndex].getKeys().Add(keyArr[i]);
                    }
                    Console.WriteLine("LeafNode[{0}]: ", currIndex);
                    leafNodes[currIndex].printAllKeys();
                    Console.WriteLine("--------------");
                    
                    leafNodes.Add(new LeafNode(new List<int>(), new List<Record>()));
                    currIndex++;
                    for (int i = (leafNodes[currIndex].getMaxNumberOfKeys() / 2) + 1;
                        i < leafNodes[currIndex].getMaxNumberOfKeys();
                        i++)
                    {
                        leafNodes[currIndex].getKeys().Add(keyArr[i]);
                    }
                    leafNodes[currIndex].getKeys().Add(keyArr[keyArr.Count-1]);
                    Console.WriteLine("LeafNode[{0}]: ", currIndex);
                    leafNodes[currIndex].printAllKeys();
                    Console.WriteLine("--------------");
                }
                
                else //when leafnode still has spaces
                {
                    leafNodes[currIndex].getKeys().Add(numVotes);
                    leafNodes[currIndex].getPointers().Add(record);
                    Console.WriteLine("LeafNodes[{0}]: ", currIndex);
                    leafNodes[currIndex].printAllKeys();
                    Console.WriteLine("--------------");
                }
            }
        }
    }
}