// using System;
// using System.Collections.Generic;
//
// namespace Project_1
// {
//     public class NonLeafNode : Node
//     {
//         private List<int> keys;
//         private List<NonLeafNode> nonLeafNodes;
//         private List<LeafNode> leafNodes;
//         private List<NonLeafNode> parent;
//         private List<NonLeafNode> children;
//         private static int maxNumberOfKeys = 3;
//         private static int maxNumberOfPointers = 4;
//         private int level;
//
//         public NonLeafNode(List<int> keys, LeafNode leafNode, List<LeafNode> leafNodes, List<NonLeafNode> nonLeafNodes, int level, List<NonLeafNode> children, List<NonLeafNode> parent)
//         {
//             this.keys = keys;
//             this.leafNodes = leafNodes;
//             this.nonLeafNodes = nonLeafNodes;
//             this.level = level;
//             this.children = children;
//             this.parent = parent;
//         }
//         
//         public int getMaxNumberOfKeys()
//         {
//             return maxNumberOfKeys;
//         }
//
//         public int getMaxNumberOfPointers()
//         {
//             return maxNumberOfPointers;
//         }
//
//         public List<int> getKeys()
//         {
//             return keys;
//         }
//         
//         // public void setKeys(List<int> keys)
//         // {
//         //     this.keys = keys;
//         // }
//
//         public List<NonLeafNode> getNonLeafNodes()
//         {
//             return this.nonLeafNodes;
//         }
//         
//         // public void setNonLeafNodes(List<NonLeafNode> nonLeafNodes)
//         // {
//         //     this.nonLeafNodes = nonLeafNodes;
//         // }
//         
//         public List<LeafNode> getLeafNodes()
//         {
//             return this.leafNodes;
//         }
//         
//         // public void setLeafNodes(List<LeafNode> leafNodes)
//         // {
//         //     this.leafNodes = leafNodes;
//         // }
//         
//         public int getLevel()
//         {
//             return this.level;
//         }
//         
//         // public void setLevel(int level)
//         // {
//         //     this.level = level;
//         // }
//
//         public void printAllKeys()
//         {
//             int count = -1;
//             foreach (int i in keys)
//             {
//                 count++;
//                 Console.WriteLine("NonLeafNode[{0}] = " + i, count);
//             }
//         }
//
//         public List<int> reOrderNode(List<int> keys)
//         {
//             keys.Sort((k1, k2) => k1.CompareTo(k2));  
//             return keys;
//         }
//
//         public bool checkNewNonLeafNeeded(int value)
//         {
//             if (value > getMaxNumberOfPointers())
//             {
//                 return true;
//             }
//             else
//             {
//                 return false;
//             }
//         }
//     }
// }