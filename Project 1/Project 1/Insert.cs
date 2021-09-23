

//using System;
//using System.Collections.Generic;

//namespace Project_1
//{
//    public struct Insert
//    {
//        private LinkedList<LeafNode> linkedLists;

//        private int index;
//        private int pointing;

//        public Insert()
//        {
//            linkedLists = null;
//            Node node = new Node(linkedLists);
//            if (node.getSize() > 3) //if node is more than 3
//            {
//                // floor function for floorfunction(n+1) + 1 
                
//            }

//        }

//        public LinkedList<LeafNode> leftNode()
//        {
//            LinkedList<LeafNode> leftNode = new LinkedList<LeafNode>();
//            return leftNode;
//        }

//        public LinkedList<LeafNode> rightNode()
//        {
//            LinkedList<LeafNode> rightNode = new LinkedList<LeafNode>();
//            return rightNode;
//        }

//        public int floorFunctionLeafNode(float value)
//        {
//            int floor = (int)Math.Floor((value + 1)/2);
//            return floor;
//        }

//        public int floorFunctionNonLeafNode(float value)
//        {
//            int floor = (int)Math.Floor(value/2);
//            return floor;
//        }

//        public bool isLeaf()
//        {
//            return false;
//        }
//    }
//}