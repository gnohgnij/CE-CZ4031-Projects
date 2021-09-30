//
//
// using System;
// using System.Collections.Generic;
// using System.Security.Cryptography.X509Certificates;
//
// namespace Project_1
// {
//     public struct Insert
//     {
//         public void isEmpty(List<LeafNode> leafNodes, int numVotes, Record record)
//         {
//             Console.WriteLine("LeafNodes == null");
//
//             leafNodes = new List<LeafNode>();
//             leafNodes.Add(new LeafNode(new List<int>(), new List<Record>(), 1));
//
//             leafNodes[0].getKeys().Add(numVotes);
//             leafNodes[0].getPointers().Add(record);
//             Console.WriteLine("LeafNodes[0]: ");
//             leafNodes[0].printAllKeys();
//             leafNodes[0].printAllRecords();
//             Console.WriteLine("--------------");
//         }
//
//         public void notEmpty(List<LeafNode> leafNodes, List<NonLeafNode> nonLeafNodes, int numVotes, Record record)
//         {
//             int[] position = search(nonLeafNodes, leafNodes, numVotes);
//             int nodeIndex = position[0];
//             int leafNodeIndex = position[1];
//
//             if (leafNodes[nodeIndex].getKeys().Count == leafNodes[nodeIndex].getMaxNumberOfKeys())
//             {
//                    
//             }
//             else
//             {
//                 leafNodes[nodeIndex].getKeys().Add(numVotes);
//                 leafNodes[nodeIndex].getPointers().Add(record);
//                 Console.WriteLine("LeafNodes[{0}]: ", nodeIndex);
//                 leafNodes[nodeIndex].printAllKeys();
//                 leafNodes[nodeIndex].printAllRecords();
//                 Console.WriteLine("--------------");
//             }
//             
//         }
//         public List<LeafNode> leftNode(List<int> keys)
//         {
//             List<LeafNode> leftNode = new List<LeafNode>();
//             return leftNode;
//         }
//
//         public List<LeafNode> rightNode()
//         {
//             List<LeafNode> rightNode = new List<LeafNode>();
//             return rightNode;
//         }
//
//         public int floorFunctionLeafNode(float value)
//         {
//             int floor = (int)Math.Floor((value + 1) / 2);
//             return floor;
//         }
//
//         public int floorFunctionNonLeafNode(float value)
//         {
//             int floor = (int)Math.Floor(value / 2);
//             return floor;
//         }
//
//         public bool isLeaf()
//         {
//             return false;
//         }
//         
//         public int[] search(List<NonLeafNode> nonLeafNodes, List<LeafNode>leafNodes, int numVotes) 
//         {
//             if (nonLeafNodes == null) 
//             {
//                 for (int i = 0; i < leafNodes.Count; i++) 
//                 {
//                     for (int j = 0; j < leafNodes[i].getKeys().Count; j++)
//                     {
//                         if (numVotes >= leafNodes[i].getKeys()[j])
//                         {
//                             return new int[] {i, j+1};
//                         }
//                     }
//                 }
//                 return new int[] {0, 0};
//             }
//             else
//             {
//                 
//             }
//         }
//     }
// }