using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace Project_1
{
    public struct BPlusTree
    {
        private List<NonLeafNode> nonLeafNodes;
        private List<LeafNode> leafNodes;

        public void insert(int numVotes, Record record)
        {
            if (leafNodes == null)  //when BPlusTree is empty
            {
                Console.WriteLine("leafNodes == null");
                this.leafNodes = new List<LeafNode>();
                leafNodes.Add(new LeafNode(new List<int>(), new List<Record>(), 1));
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
                    List<Record> recordArr = new List<Record>();
                    
                    foreach (int i in leafNodes[currIndex].getKeys())
                    {
                        keyArr.Add(i);
                    }

                    foreach (Record r in leafNodes[currIndex].getPointers())
                    {
                        recordArr.Add(r);
                    }
                    leafNodes[currIndex].getKeys().Clear();
                    leafNodes[currIndex].getPointers().Clear();
                    
                    keyArr.Add(numVotes);
                    recordArr.Add(record);
                    
                    keyArr.Sort((k1, k2) => k1.CompareTo(k2));
                    recordArr.Sort((r1, r2) => r1.getNumVotes().CompareTo(r2.getNumVotes()));
                    
                    for (int i = 0; i < (leafNodes[currIndex].getMaxNumberOfKeys() / 2) + 1; i++)
                    {
                        leafNodes[currIndex].getKeys().Add(keyArr[i]);
                        leafNodes[currIndex].getPointers().Add(recordArr[i]);
                    }
                    Console.WriteLine("LeafNode[{0}]: ", currIndex);
                    leafNodes[currIndex].printAllKeys();
                    leafNodes[currIndex].printAllRecords();
                    Console.WriteLine("--------------");
                    
                    leafNodes.Add(new LeafNode(new List<int>(), new List<Record>(), 1));
                    increaseLevel(this.leafNodes);

                    this.nonLeafNodes = new List<NonLeafNode>();
                    nonLeafNodes.Add(new NonLeafNode(1));
                    nonLeafNodes[0].setKeys(new List<int>());

                    currIndex++;
                    for (int i = (leafNodes[currIndex].getMaxNumberOfKeys() / 2) + 1;
                        i < leafNodes[currIndex].getMaxNumberOfKeys();
                        i++)
                    {
                        leafNodes[currIndex].getKeys().Add(keyArr[i]);
                        leafNodes[currIndex].getPointers().Add(recordArr[i]);
                    }
                    leafNodes[currIndex].getKeys().Add(keyArr[keyArr.Count-1]);
                    leafNodes[currIndex].getPointers().Add(recordArr[recordArr.Count-1]);

                    //nonLeafNodes[0].getKeys().Add(leafNodes[currIndex].getKeys()[0]);

                    Console.WriteLine("LeafNode[{0}]: ", currIndex);
                    leafNodes[currIndex].printAllKeys();
                    leafNodes[currIndex].printAllRecords();
                    Console.WriteLine("--------------");

                    //nonLeafNodes[0].printAllKeys();


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

        public void increaseLevel(List<LeafNode> leafNodes)
        {
            foreach (LeafNode l in leafNodes)
            {
                l.setLevel(l.getLevel()+1);
                Console.WriteLine("Leaf Node Level = " + l.getLevel());
            }
        }
    }
}