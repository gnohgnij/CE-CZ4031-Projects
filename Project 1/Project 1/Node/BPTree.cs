using System;
using System.Collections.Generic;

namespace Project_1.Node
{
    public class BPTree
    {
        private int maxChildLimit;
        private int maxLeafNodeLimit;
        private BPlusTreeNode root;

        public void search(int key)
        {
            if (root == null)
            {
                Console.WriteLine("No nodes");
            }
            else
            {
                BPlusTreeNode cursor = root;
                List<int> temp = cursor.getAllKeys();
                while (cursor.checkIsLeaf() == false)
                {   
                    int index = -1;
                    foreach (int i in temp)
                    {
                        index++;
                        if (i >= key)
                            break;
                    }
                    Console.WriteLine("Index to traverse to: " + index);

                    cursor = cursor.getPointer2TreeOrData().getPointer2InternalNodes()[index];
                }

                temp = cursor.getAllKeys();
                int leafNodeIndex = -1;
                foreach (int i in temp)
                {
                    leafNodeIndex++;
                    if (i == key)
                        break;
                }
                Console.WriteLine("The key should be found at leaf node index: " + leafNodeIndex);
                if (cursor.getAllKeys()[leafNodeIndex] != key)
                {
                    Console.WriteLine("The key is not found");
                }
                else
                {
                    Console.WriteLine("Key found!");
                    //TODO: search record to print
                }
            }
        }
        
        public void insert(int key, Record record)
        {
            if (root == null)
            {
                root = new BPlusTreeNode(new List<int>());
                root.setIsLeaf(true);
                root.getAllKeys().Add(key);
                root.getPointer2TreeOrData().getPointer2Records().Add(record);
                Console.WriteLine("I AM ROOT!");
            }
            else
            {
                
            }
        }
    }
    
    
}