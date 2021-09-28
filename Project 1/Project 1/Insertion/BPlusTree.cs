using System;
using System.Collections;
using System.Collections.Generic;

namespace Project_1
{
    public struct BPlusTree
    {
        /*
        * Instantiation
        */

        private List<NonLeafNode> nonLeafNodes;
        private List<LeafNode> leafNodes;

        /* 
        * Insert Function
        */

        public void insert(int numVotes, Record record)
        {
            /*
            * When LeafNode is empty
            */
            if (leafNodes == null) 
            {
                Console.WriteLine("LeafNodes == null");

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
                int[] indexIJ = search(numVotes);
                int nodeIndex = indexIJ[0];  
                int leafNodeIndex = indexIJ[1];

                /*
                * When LeafNode reaches max capacity
                */
                if (leafNodes[nodeIndex].getKeys().Count == leafNodes[0].getMaxNumberOfKeys())  
                {
                    List<int> keyArr = new List<int>();
                    List<Record> recordArr = new List<Record>();
                    
                    foreach (int i in leafNodes[nodeIndex].getKeys())
                    {
                        keyArr.Add(i);
                    }
                    foreach (Record r in leafNodes[nodeIndex].getPointers())
                    {
                        recordArr.Add(r);
                    }

                    leafNodes[nodeIndex].getKeys().Clear();
                    leafNodes[nodeIndex].getPointers().Clear();
                    keyArr.Add(numVotes);
                    recordArr.Add(record);
                    keyArr.Sort((k1, k2) => k1.CompareTo(k2));
                    recordArr.Sort((r1, r2) => r1.getNumVotes().CompareTo(r2.getNumVotes()));
                    
                    for (int i = 0; i < (leafNodes[nodeIndex].getMaxNumberOfKeys() / 2) + 1; i++)
                    {
                        leafNodes[nodeIndex].getKeys().Add(keyArr[i]);
                        leafNodes[nodeIndex].getPointers().Add(recordArr[i]);
                    }
                    Console.WriteLine("LeafNode[{0}]: ", nodeIndex);
                    leafNodes[nodeIndex].printAllKeys();
                    leafNodes[nodeIndex].printAllRecords();
                    Console.WriteLine("--------------");
                    leafNodes.Add(new LeafNode(new List<int>(), new List<Record>(), 1));
                    nodeIndex++;
                    if (leafNodes[nodeIndex].getKeys() == null)
                    {
                        for (int i = (leafNodes[nodeIndex].getMaxNumberOfKeys() / 2) + 1;
                        i < leafNodes[nodeIndex].getMaxNumberOfKeys();
                        i++)
                        {
                            leafNodes[nodeIndex].getKeys().Add(keyArr[i]);
                            leafNodes[nodeIndex].getPointers().Add(recordArr[i]);
                        }
                        leafNodes[nodeIndex].getKeys().Add(keyArr[keyArr.Count-1]);
                        leafNodes[nodeIndex].getPointers().Add(recordArr[recordArr.Count-1]);
                        Console.WriteLine("LeafNode[{0}]: ", nodeIndex);
                        leafNodes[nodeIndex].printAllKeys();
                        leafNodes[nodeIndex].printAllRecords();
                        Console.WriteLine("--------------");
                        //
                        // if ()
                        //{
                        //NonLeafNode nonLeafNode = new NonLeafNode();

                        //}
                    }
                    else
                    { // Enters when splitting node
                        Console.WriteLine("Entered");
                        nodeIndex++;
                        leafNodes.Add(new LeafNode(new List<int>(), new List<Record>(), 1));
                        for (int i = 0; i < leafNodes[nodeIndex-1].getKeys().Count; i++)
                        {
                            leafNodes[nodeIndex].getKeys().Add(leafNodes[nodeIndex-1].getKeys()[i]);
                            leafNodes[nodeIndex].getPointers().Add(leafNodes[nodeIndex-1].getPointers()[i]);
                        }
                        Console.WriteLine("LeafNode[{0}]: ", nodeIndex);
                        leafNodes[nodeIndex].printAllKeys();
                        leafNodes[nodeIndex].printAllRecords();
                        Console.WriteLine("--------------");
                        leafNodes[nodeIndex-1].getKeys().Clear();
                        leafNodes[nodeIndex-1].getPointers().Clear();
                        for (int i = (leafNodes[nodeIndex].getMaxNumberOfKeys() / 2) + 1;
                            i < leafNodes[nodeIndex].getMaxNumberOfKeys();
                            i++)
                        {
                            leafNodes[nodeIndex-1].getKeys().Add(keyArr[i]);
                            leafNodes[nodeIndex-1].getPointers().Add(recordArr[i]);
                        }
                        leafNodes[nodeIndex-1].getKeys().Add(keyArr[keyArr.Count-1]);
                        leafNodes[nodeIndex-1].getPointers().Add(recordArr[recordArr.Count-1]);
                        Console.WriteLine("LeafNode[{0}]: ", nodeIndex-1);
                        leafNodes[nodeIndex-1].printAllKeys();
                        leafNodes[nodeIndex-1].printAllRecords();
                        Console.WriteLine("--------------");
                    }

                    // this.nonLeafNodes = new List<NonLeafNode>();
                    // nonLeafNodes.Add(new NonLeafNode(new List<int>(), new List<LeafNode>(), new List<NonLeafNode>(), 1));

                    // for (int i = (leafNodes[nodeIndex].getMaxNumberOfKeys() / 2) + 1;
                    //     i < leafNodes[nodeIndex].getMaxNumberOfKeys();
                    //     i++)
                    // {
                    //     leafNodes[nodeIndex].getKeys().Add(keyArr[i]);
                    //     leafNodes[nodeIndex].getPointers().Add(recordArr[i]);
                    // }
                    // leafNodes[nodeIndex].getKeys().Add(keyArr[keyArr.Count-1]);
                    // leafNodes[nodeIndex].getPointers().Add(recordArr[recordArr.Count-1]);

                    // nonLeafNodes[0].getKeys().Add(leafNodes[currIndex].getKeys()[0]);

                    Console.WriteLine("LeafNode[{0}]: ", nodeIndex);
                    leafNodes[nodeIndex].printAllKeys();
                    leafNodes[nodeIndex].printAllRecords();
                    Console.WriteLine("--------------");
                    // nonLeafNodes[0].printAllKeys();
                }


                /*
                * When LeafNode has empty space
                */
                else 
                {
                    if (leafNodeIndex == 0) {
                        List<int> tempNumVotes = new List<int>();
                        List<Record> tempRecords = new List<Record>();
                        tempNumVotes.Add(numVotes);
                        tempRecords.Add(record);

                        foreach (int i in leafNodes[nodeIndex].getKeys())
                        {
                            tempNumVotes.Add(i);
                        }
                        foreach (Record r in leafNodes[nodeIndex].getPointers())
                        {
                            tempRecords.Add(r);
                        }
                        leafNodes[nodeIndex].getKeys().Clear();
                        leafNodes[nodeIndex].getPointers().Clear();
                    }

                    leafNodes[nodeIndex].getKeys().Add(numVotes);
                    leafNodes[nodeIndex].getPointers().Add(record);

                    leafNodes[nodeIndex].reOrderNode(leafNodes[nodeIndex].getKeys());

                    Console.WriteLine("LeafNodes[{0}]: ", nodeIndex);
                    leafNodes[nodeIndex].printAllKeys();
                    leafNodes[nodeIndex].printAllRecords();
                    Console.WriteLine("--------------");
                }
            }
        }
         
        public int[] search(int numVotes) 
        {
            if (nonLeafNodes == null) 
            {
                for (int i = 0; i < leafNodes.Count; i++) 
                {
                    for (int j = 0; j < leafNodes[i].getKeys().Count; j++)
                    {
                        if (numVotes >= leafNodes[i].getKeys()[j])
                        {
                            return new int[] {i, j+1};
                        }
                    }
                }
                return new int[] {0, 0};
            }
            return new int[] {0, 0};
            // else
            // {

            // }
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