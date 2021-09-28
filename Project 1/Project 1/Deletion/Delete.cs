<<<<<<< HEAD


using System;
using System.Collections.Generic;

namespace Project_1
{
    public struct Delete
    {
        private List<int> keys;
        private List<Record> pointers;

        //private int index;
        // private int pointing;

        public Delete(List<int> keys, List<Record> pointers)
        {
            this.keys = null;
            this.pointers = null;
            LeafNode leafNode = new LeafNode(keys, pointers);

            if (leafNode.getMaxNumberOfKeys() > 3)
            {
                //leftNode();
                //leftNode = floorFunctionLeafNode(keys.Count);
            }
            NonLeafNode nonLeafNode = new NonLeafNode(keys, pointers);
            if (nonLeafNode.getMaxNumberOfKeys() > 3)
            {
                // rightNode = floorFunctionNonLeafNode(keys.Count);
            }
        }
    }
}
=======
//
//
// using System;
// using System.Collections.Generic;
//
// namespace Project_1
// {
//     public struct Delete
//     {
//         private List<int> keys;
//         private List<Record> pointers;
//
//         //private int index;
//         // private int pointing;
//
//         public Delete(List<int> keys, List<Record> pointers)
//         {
//             this.keys = null;
//             this.pointers = null;
//             LeafNode leafNode = new LeafNode(keys, pointers);
//
//             if (leafNode.getMaxNumberOfKeys() > 3)
//             {
//                 //leftNode();
//                 //leftNode = floorFunctionLeafNode(keys.Count);
//             }
//             NonLeafNode nonLeafNode = new NonLeafNode(keys, pointers);
//             if (nonLeafNode.getMaxNumberOfKeys() > 3)
//             {
//                 rightNode();
//                 // rightNode = floorFunctionNonLeafNode(keys.Count);
//             }
//
//
//         }
//
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
//     }
// }
>>>>>>> 78ff0e76fb249c5b9d800d3bdb4cb54b8e01d7de
