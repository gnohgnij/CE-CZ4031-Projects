using System;
using System.Collections.Generic;

namespace Project_1.Node
{
    public class BPTree
    {
        private static int maxChildLimit = 4;
        private static int maxLeafNodeLimit = 3;   //to change
        private BPlusTreeNode root;
        
        /*
         * function to get maxLeafNodeLimit
         */
        //TODO: implement this function
        
        /*
         * function to get maxChildLimit
         */
        //TODO: implement this function
        
        /*
         * function to set maxLeafNodeLimit
         */
        //TODO: implement this function
        
        /*
         * function to set maxChildLimit
         */
        //TODO: implement this function
        
        /*
         * function to set the root of the B+ Tree
         */
        public void setRoot(BPlusTreeNode root)
        {
            this.root = root;
        }
        
        /*
         * function that returns root of B+ Tree
         */
        public BPlusTreeNode getRoot()
        {
            return this.root;
        }
        
        /*
         * function that finds the index that the key should be into
         */
        public int findIndex(List<int> list, int key)
        {
            int index = 0;
            foreach (int i in list)
            {
                if (key < i)
                    break;
                else if (key == i)
                {
                    index++;
                    break;
                }
                //if key > current i : run another iteration
                index++;
            }
            return index;
        }

        /*
         * function to print entire tree from root to leaf nodes
         */
        public void printTree(BPlusTreeNode cursor)
        {
            if (cursor == null) return;
            Queue<BPlusTreeNode> q = new Queue<BPlusTreeNode>();
            q.Enqueue(cursor);

            while (q.Count > 0)
            {
                int size = q.Count;
                for (int i = 0; i<size; i++)
                {
                    BPlusTreeNode u = q.Peek();
                    q.Dequeue();
                    
                    Console.Write("|");
                    foreach (int val in u.getAllKeys())
                    {
                        Console.Write(val + " ");
                    }
                    Console.Write("|");
                    if (u.checkIsLeaf() == false)
                    {
                        foreach (BPlusTreeNode v in u.getPointer2TreeOrData(null, null).getPointer2InternalNodes())
                        {
                            q.Enqueue(v);
                        }
                    }
                }
                Console.WriteLine("");
            }
        }

        /*
         * function to search for a key in the B+ Tree
         */
        public void search(int key)
        {
            Console.WriteLine("Searching for key: {0} ...", key);
            if (root == null)
            {
                Console.WriteLine("B+ Tree is empty, insert keys first!");
            }
            else
            {
                BPlusTreeNode cursor = root;
                while (cursor.checkIsLeaf() == false)
                {
                    List<int> temp = cursor.getAllKeys();
                    int index = findIndex(temp, key);
                    
                    //go to child node
                    cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[index];
                    Console.WriteLine("1st key of next node = " + cursor.getAllKeys()[0]);
                }
                List<int> leafNodes = cursor.getAllKeys();
                int leafNodeIndex = 0;
                for (int i = 0; i < leafNodes.Count; i++)
                {
                    if (key == leafNodes[i])
                        leafNodeIndex = i;
                }
                
                //key not found
                if (leafNodes[leafNodeIndex] != key)
                {
                    Console.WriteLine("Key not found");
                }
                
                //key found
                else
                {
                    Console.WriteLine("Key found!");
                    Record found = cursor.getPointer2TreeOrData(null, null).getPointer2Records()[leafNodeIndex];
                    Console.Write("Record details: ");
                    found.printRecord();
                }
            }
        }
        
        /*
         * function to insert a new key into the B+ Tree, along with its record
         */
        public void insert(int key, Record record)
        {
            //if the tree is empty, create new root node, add key into root node and point to record
            if (root == null)
            {
                root = new BPlusTreeNode(new List<int>());
                root.setIsLeaf(true);
                root.getAllKeys().Add(key);
                root.getPointer2TreeOrData(new List<BPlusTreeNode>(), new List<Record>())
                    .getPointer2Records().Add(record);
                Console.WriteLine("Root node initialised, B+ Tree created");
            }
            
            // if the tree is not empty and already has keys in its root node
            else
            {
                BPlusTreeNode cursor = root;
                BPlusTreeNode parent = null;
                //traverse to the leaf node where the key should be inserted into
                while (cursor.checkIsLeaf() == false)
                {
                    parent = cursor;
                    List<int> temp = cursor.getAllKeys();
                    int index = findIndex(temp, key);
                    cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[index];
                }
                //if the leaf node has empty space
                if (cursor.getAllKeys().Count < maxLeafNodeLimit)
                {
                    List<int> temp = cursor.getAllKeys();
                    int index = findIndex(temp, key);
                    //if key to be inserted is > all the keys inside the leaf node, add the key to the end of the leaf node
                    if (index == cursor.getAllKeys().Count)
                    {
                        cursor.getAllKeys().Add(key);
                        cursor.getPointer2TreeOrData(null, null).getPointer2Records().Add(record);
                    }

                    //if key is to be inserted in the middle of the leaf node, insert the key at the correct index, push back the rest
                    // of the keys

                    else if (index < cursor.getAllKeys().Count)
                    {
                        cursor.getAllKeys().Insert(index, key);
                        cursor.getPointer2TreeOrData(null, null).getPointer2Records().Insert(index, record);
                        Console.WriteLine("Inserted key {0} successfully at index {1}", key, index);
                    }
                }
                
                //if the leaf node has no empty space
                else
                {  
                    List<int> virtualNode = new List<int>();
                    List<Record> virtualDataNode = new List<Record>();
                    
                    //store all the keys and records in a temporary list each
                    for (int i = 0; i < cursor.getAllKeys().Count; i++)
                    {
                        virtualNode.Add(cursor.getAllKeys()[i]);
                        virtualDataNode.Add(cursor.getPointer2TreeOrData(null,null).getPointer2Records()[i]);
                    }
                    
                    List<int> temp = cursor.getAllKeys();
                    int index = findIndex(temp, key);
                    
                    //if key is > all other keys
                    if (index == virtualNode.Count)
                    {
                        virtualNode.Add(key);
                        virtualDataNode.Add(record);
                    }

                    //if key is to be inserted in the middle of the leaf node, push back rest of keys, insert key in
                    //correct index
                    else if (index < virtualNode.Count)
                    {
                        virtualNode.Insert(index, key);
                        virtualDataNode.Insert(index, record);
                    }
                    
                    //create new leaf node
                    BPlusTreeNode newLeaf = new BPlusTreeNode(new List<int>());
                    newLeaf.setIsLeaf(true);
                    newLeaf.getPointer2TreeOrData(null, new List<Record>());
                    
;                    //swapping pointers
                    BPlusTreeNode temporary = cursor.getPointer2Next(); 
                    cursor.setPointer2Next(newLeaf); 
                    newLeaf.setPointer2Next(temporary);

                    //put first n/2 + 1 keys into old leaf node
                    cursor.getAllKeys().Clear();
                    cursor.getPointer2TreeOrData(null, null).getPointer2Records().Clear();
                    for (int i = 0; i < maxLeafNodeLimit / 2 + 1; i++)
                    {
                        cursor.getAllKeys().Add(virtualNode[i]);
                        cursor.getPointer2TreeOrData(null, null).getPointer2Records().Add(virtualDataNode[i]);
                    }

                    //put remaining keys into new leaf node
                    for (int i = maxLeafNodeLimit / 2 + 1; i < virtualNode.Count; i++)
                    {
                        newLeaf.getAllKeys().Add(virtualNode[i]);
                        newLeaf.getPointer2TreeOrData(null, null).getPointer2Records().Add(virtualDataNode[i]);
                    }

                    //if leaf node being split is also the root node, create new root node, store 1st index of the new
                    //leaf node into the root node
                    if (cursor == root)
                    {
                        Console.WriteLine("Leaf node == Root node");
                        BPlusTreeNode newRoot = new BPlusTreeNode(new List<int>());
                        newRoot.getAllKeys().Add(newLeaf.getAllKeys()[0]);
                        newRoot.getPointer2TreeOrData(new List<BPlusTreeNode>(), null).getPointer2InternalNodes()
                            .Add(cursor);
                        newRoot.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Add(newLeaf);
                        root = newRoot;
                        Console.WriteLine("Created new root");
                    }
                    
                    //leaf node being split is not root node, store 1st index of new leaf node into internal node
                    else
                    {
                        Console.WriteLine("Leaf node != Root node, putting key {0} into Internal node...", key);
                        insertInternal(newLeaf.getAllKeys()[0], parent, newLeaf);
                    }
                }
            }
        }
        
        /*
         * function to insert key into an internal node
         */
        public void insertInternal(int key, BPlusTreeNode cursor, BPlusTreeNode child)
        {
            // if internal node has empty space
            if (cursor.getAllKeys().Count < maxChildLimit - 1)
            {

                List<int> temp = cursor.getAllKeys();
                int index = findIndex(temp, key);

                //if key > all other keys
                if (index == cursor.getAllKeys().Count)
                {
                    cursor.getAllKeys().Add(key);
                    cursor.getPointer2TreeOrData(new List<BPlusTreeNode>(), null).
                        getPointer2InternalNodes().Add(child);
                }

                //else if key is to be inserted in the middle of the internal node, push back other keys, insert key in
                //correct index
                else if (index < cursor.getAllKeys().Count)
                {
                    cursor.getAllKeys().Insert(index, key);
                    cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Insert(index + 1, child);
                }

                Console.WriteLine("Inserted key {0} in the internal node successfully", key);
            }
            

            //if internal node has no empty space
            else
            {
                Console.WriteLine("Overflow in internal, splitting internal nodes");
                
                //hold all keys and records in a temporary list each
                List<int> virtualKeyNode = new List<int>();
                List<BPlusTreeNode> virtualTreePtrNode = new List<BPlusTreeNode>();
                foreach (int i in cursor.getAllKeys())
                {
                    virtualKeyNode.Add(i);
                }
                foreach (BPlusTreeNode i in cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes())
                {
                    virtualTreePtrNode.Add(i);
                }
                
                List<int> temp = cursor.getAllKeys();
                int index = findIndex(temp, key);
                
                //if key > are other keys, insert at the back of internal node
                if (index == cursor.getAllKeys().Count)
                {
                    virtualKeyNode.Add(key);
                    virtualTreePtrNode.Add(child);
                }
                
                //else if key is to be inserted in the middle of internal node, push back all other nodes, insert key
                //in correct index
                else if (index < virtualKeyNode.Count)
                {
                    virtualKeyNode.Insert(index, key);
                    virtualTreePtrNode.Insert(index+1, child);
                }
                
                //get index and key to split the internal node
                int partitionKey = virtualKeyNode[virtualKeyNode.Count / 2];
                int partitionIndex = virtualKeyNode.Count / 2;

                cursor.getAllKeys().Clear();
                cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Clear();
                
                //first internal node get first half of keys
                for (int i = 0; i < partitionIndex; i++)
                {
                    cursor.getAllKeys().Add(virtualKeyNode[i]);
                }
                for (int i = 0; i < partitionIndex + 1; i++)
                {
                    cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Add(virtualTreePtrNode[i]);
                }

                //create new internal node, insert other half of keys
                BPlusTreeNode newInternalNode = new BPlusTreeNode(new List<int>());
                for (int i = partitionIndex + 1; i < virtualKeyNode.Count; i++)
                {
                    newInternalNode.getAllKeys().Add(virtualKeyNode[i]);
                }
                for (int i = partitionIndex + 1; i < virtualTreePtrNode.Count; i++)
                {
                    newInternalNode.getPointer2TreeOrData(new List<BPlusTreeNode>(),null).getPointer2InternalNodes().Add(virtualTreePtrNode[i]);
                }
                
                if (cursor == root)
                {
                    BPlusTreeNode newRoot = new BPlusTreeNode(new List<int>());
                    newRoot.getAllKeys().Add(partitionKey);
                    newRoot.getPointer2TreeOrData(new List<BPlusTreeNode>(), null).getPointer2InternalNodes().Add(cursor);
                    newRoot.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Add(newInternalNode);
                    root = newRoot;
                    Console.WriteLine("Created New Root");
                }
                else
                {
                     insertInternal(partitionKey, findParent(root, cursor), newInternalNode);
                }
            }
        }
        
        /*
         * function to find the parent node of a child node
         */
        public BPlusTreeNode findParent(BPlusTreeNode cursor, BPlusTreeNode child)
        {
            BPlusTreeNode parent = null;
            if (cursor.checkIsLeaf() ||
                cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[0].checkIsLeaf())
            {
                return null;
            }

            for (int i = 0; i < cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Count; i++)
            {
                if (cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i] == child)
                {
                    parent = cursor;
                    break;
                }

                else
                {
                    BPlusTreeNode tempCursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i];
                    BPlusTreeNode temp = findParent(tempCursor, child);

                    if(temp != null)
                    {
                        return temp;
                    }
                }
            }
            return parent;
        }
        
        /*
         * function to delete a key from the B+ Tree
         */
        public void delete(int index)
        {
            BPlusTreeNode root = getRoot();
            if (root == null)
            {
                Console.WriteLine("B+ Tree is empty.");
                return;
            }
        
            BPlusTreeNode cursor = root;
            BPlusTreeNode parent = null;
            int leftSibling = -1, rightSibling = -1;
        
            while (cursor.checkIsLeaf() != true)
            {
                for (int i = 0; i < cursor.getAllKeys().Count; i++)
                {
                    parent = cursor;
                    leftSibling = i - 1;
                    rightSibling = i + 1;
        
                    if (index < cursor.getAllKeys()[i])
                    {

                        cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i];
                        break;
                    }
                    if (i == cursor.getAllKeys().Count - 1)
                    {
                        leftSibling = i;
                        rightSibling = i + 2;
                        cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i + 1];
                        break;
                    }
                }
            }
        
            // Check if the value exists in this leaf node
            int pos = 0;
            bool found = false;
            for (pos = 0; pos < cursor.getAllKeys().Count; pos++)
            {
                if (cursor.getAllKeys()[pos] == index)
                {
                    found = false;   //kiv
                    break;
                }
            }
        
            //  var itr = lower_bound
            int itr = 0;
            List<int> temp = cursor.getAllKeys();

            foreach (int i in temp)
            {
                if (index <= i)
                    break;
                //if key > current i : run another iteration
                itr++;
            }
            if (index > temp[temp.Count-1])
            {
                itr++;
            }
        
            if (itr == cursor.getAllKeys().Count)
            {
                Console.WriteLine("Key not found!");
                return;
            }
        
            // Delete the respective key and record
            cursor.getAllKeys().RemoveAt(itr);    //remove key
            cursor.getPointer2TreeOrData(null, null).getPointer2Records().RemoveAt(itr);
            
            //shifting keys and records
            // for (int i = pos; i < cursor.getAllKeys().Count - 1; i++)
            // {
            //     cursor.getAllKeys()[i] = cursor.getAllKeys()[i + 1];
            //     cursor.getPointer2TreeOrData(null, null).getPointer2Records()[i] = cursor.getPointer2TreeOrData(null, null).getPointer2Records()[i + 1];
            // }
            
            //int prev_size = cursor.getAllKeys().Count;
            //cursor->keys.resize(prev_size - 1);
            //cursor->ptr2TreeOrData.dataPtr.resize(prev_size - 1);
            
            //if leaf node is root node
            if (cursor == root)
            {
                if (cursor.getAllKeys().Count == 0)
                {
                    setRoot(null);
                    Console.Write("B+ Tree is now empty...");
                }
            }
        
            Console.WriteLine("Deleted {0} from leaf node successfully!", index);
            if (cursor.getAllKeys().Count >= (maxLeafNodeLimit + 1) / 2)    //was +1
            {
                return;
            }
            Console.WriteLine("Underflow in the leaf node happened");
            Console.WriteLine("Starting redistribution...");
            
            //try borrowing key from left sibling node
            if (leftSibling >= 0)
            {
                BPlusTreeNode leftNode = parent.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[leftSibling];
                
                //if left sibling node has enough keys to lend
                if (leftNode.getAllKeys().Count >= (maxLeafNodeLimit + 1) / 2 + 1)
                {
                    //Borrow largest key from left sibling node
                    int maxIndex = leftNode.getAllKeys().Count - 1;
                    int keyToBorrow = leftNode.getAllKeys()[maxIndex];
                    
                    //insert new key at first index
                    cursor.getAllKeys().Insert(0, keyToBorrow);
                    cursor.getPointer2TreeOrData(null, null).getPointer2Records().Insert(
                        0, leftNode.getPointer2TreeOrData(null, null).getPointer2Records()[maxIndex]);
                    
                    //remove the borrowed key from left sibling
                    leftNode.getAllKeys().RemoveAt(maxIndex);
                    leftNode.getPointer2TreeOrData(null, null).getPointer2Records().
                        RemoveAt(maxIndex);
                    
                    //update parent node
                    parent.getAllKeys().RemoveAt(leftSibling);
                    parent.getAllKeys().Insert(leftSibling, cursor.getAllKeys()[0]);
                    
                    Console.WriteLine("Borrowed key from left sibling node!");
                    return;
                    
                    //int maxIndex = leftNode.getAllKeys().Count - 1;
                    //cursor->keys.insert(cursor->keys.begin(), leftNode->keys[maxIdx]);
                    //cursor->ptr2TreeOrData.dataPtr
                    //    .insert(cursor->ptr2TreeOrData.dataPtr.begin(), leftNode->ptr2TreeOrData.dataPtr[maxIdx]);
        
                    ////resize the left Sibling Node After Tranfer
                    //leftNode->keys.resize(maxIdx);
                    //leftNode->ptr2TreeOrData.dataPtr.resize(maxIdx);
                    // parent.getAllKeys()[leftSibling] = cursor.getAllKeys()[0];
                    // Console.WriteLine("Transferred from left sibling of leaf node");
                    // return;
                }       
            }
            
            //try to borrow key from right sibling node
            else if (rightSibling < parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count)
            {
                BPlusTreeNode rightNode = parent.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[rightSibling];
                
                //if right sibling node has enough keys to lend
                if (rightNode.getAllKeys().Count >= (maxLeafNodeLimit + 1) / 2 + 1)
                {
                    //borrow the smallest key from right sibling node
                    int minIndex = 0;
                    int keyToBorrow = rightNode.getAllKeys()[minIndex];
                    
                    cursor.getAllKeys().Add(keyToBorrow);
                    cursor.getPointer2TreeOrData(null, null).getPointer2Records().
                        Add(rightNode.getPointer2TreeOrData(null, null).
                            getPointer2Records()[minIndex]);
                    
                    //remove the borrowed key from right sibling node
                    rightNode.getAllKeys().RemoveAt(minIndex);
                    rightNode.getPointer2TreeOrData(null, null).getPointer2Records().
                        RemoveAt(minIndex);
                    
                    //update parent node
                    parent.getAllKeys().RemoveAt(rightSibling-1);
                    parent.getAllKeys().Insert(rightSibling-1, rightNode.getAllKeys()[0]);
                    return;

                    //resize the right Sibling Node After Tranfer
                    //rightNode->keys.erase(rightNode->keys.begin());
                    //rightNode->ptr2TreeOrData.dataPtr.erase(rightNode->ptr2TreeOrData.dataPtr.begin());//resize the right Sibling Node After Tranfer
                    //rightNode->keys.erase(rightNode->keys.begin());
                    //rightNode->ptr2TreeOrData.dataPtr.erase(rightNode->ptr2TreeOrData.dataPtr.begin());

                    // parent.getAllKeys()[rightSibling - 1] = rightNode.getAllKeys()[0];
                    // Console.WriteLine("Transferred from right sibling of lead node");
                    // return;
                }
            }

            //if can borrow from neither sibling nodes, merge with sibling node and delete
            else
            {
                if (leftSibling >= 0)
                {
                    BPlusTreeNode leftNode = parent.getPointer2TreeOrData(null, null).
                        getPointer2InternalNodes()[leftSibling];
        
                    for (int i = 0; i < cursor.getAllKeys().Count; i++)
                    {
                        //transfer key and records to left sibling node and connect pointer2next
                        leftNode.getAllKeys().Add(cursor.getAllKeys()[i]);
                        leftNode.getPointer2TreeOrData(null, null).getPointer2Records().
                            Add(cursor.getPointer2TreeOrData(null, null).getPointer2Records()[i]);
                    }
        
                    leftNode.setPointer2Next(cursor.getPointer2Next());
                    Console.WriteLine("Merging two leaf nodes");
                    removeInternal(parent.getKey(leftSibling), parent, cursor);
                }
                
                else if (rightSibling <= parent.getAllKeys().Count)
                {
                    BPlusTreeNode rightNode = parent.getPointer2TreeOrData(null, null).
                            getPointer2InternalNodes()[rightSibling];
                    
                    //transfer key and record to right sibling node and connect pointer2next;
                    for (int i = 0; i < rightNode.getAllKeys().Count; i++)
                    {
                        cursor.getAllKeys().Add(rightNode.getKey(i));
                        Console.WriteLine("Merging two leaf nodes");
                        removeInternal(parent.getKey(rightSibling-1), parent, rightNode);
                    }
                }
            }
            
        }
        
        /*
         * function to remove a key from an internal node
         */
        public void removeInternal(int key, BPlusTreeNode cursor, BPlusTreeNode child)
        {
            BPlusTreeNode root = getRoot();
        
            //check if key from root is to be deleted
            if(cursor == root)
            {
                if(cursor.getAllKeys().Count == 1)
                {
                    if(cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[1] == child)
                    {
                        //if only 1 key is left and matches with one of the child nodes
                        setRoot(cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[0]);
                        Console.WriteLine("Root node is changed");
                        return;
                    }
                    else if(cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[0] == child)
                    {
                        setRoot(cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[1]);
                        Console.WriteLine("Root node is changed");
                        return;
                    }
                }
            }
        
            // Deleting key from the parent
            int pos;
            for (pos = 0; pos < cursor.getAllKeys().Count; pos++)
            {
                if (cursor.getAllKeys()[pos] == key)
                {
                    break;
                }
            }
            for (int i = pos; i < cursor.getAllKeys().Count - 1; i++)
            {
                // cursor.getAllKeys()[i] = cursor.getAllKeys()[i + 1];
                int k = cursor.getKey(i + 1);
                cursor.getAllKeys().RemoveAt(i);
                cursor.getAllKeys().Insert(i, k);
            }
            // cursor.getAllKeys().resize(cursor.getAllKeys().Count - 1);
        
            // Now deleting the pointer2tree
            for (pos = 0; pos < cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count; pos++)
            {
                if (cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[pos] == child)
                {
                    break;
                }
            }
        
            for (int i = pos; i < cursor.getPointer2TreeOrData(null,null).
                getPointer2InternalNodes().Count - 1; i++)
            {
                // cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[i] = cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[i + 1];
                BPlusTreeNode k = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i + 1];
                cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().RemoveAt(i);
                cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Insert(i, k);
            }
            //cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()
            //    .resize(cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count - 1);
        
            //if no underflow
            if (cursor.getAllKeys().Count >= (maxChildLimit + 1) / 2 - 1)
            {
                Console.WriteLine("Deleted " + key + "from internal node succesfully");
                return;
            }
        
            else
            {
                Console.WriteLine("UnderFlow in internal node");
                
                if (cursor == root)
                {
                    return;
                }
        
                BPlusTreeNode p1 = findParent(root, cursor);
                BPlusTreeNode parent = p1;
                // BPlusTreeNode parent = null;
            
                int leftSibling = - 1, rightSibling = -1;
            
                // Finding Left and Right Siblings as we did earlier
                for (pos = 0; pos < parent.getPointer2TreeOrData(null,null).
                    getPointer2InternalNodes().Count; pos++)
                {
                    if (parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[pos] == cursor)
                    {
                        leftSibling = pos - 1;
                        rightSibling = pos + 1;
                        break;
                    }
                }
        
                // If possible transfer to leftSibling
                if (leftSibling >= 0)
                {
                    BPlusTreeNode leftNode = parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[leftSibling];
            
                    //Check if LeftSibling has extra Key to transfer
                    if (leftNode.getAllKeys().Count >= (maxChildLimit + 1) / 2)
                    {
            
                        //transfer key from left sibling through parent
                        int maxIdxKey = leftNode.getAllKeys().Count - 1;
                        cursor.getAllKeys().Insert(0, parent.getKey(leftSibling));
                        int temp = leftNode.getKey(maxIdxKey);
                        parent.getAllKeys().RemoveAt(leftSibling);
                        parent.getAllKeys().Insert(leftSibling, temp);
                        // cursor->keys.insert(cursor->keys.begin(), parent->keys[leftSibling]);
                        // parent.getAllKeys()[leftSibling] = leftNode.getAllKeys()[maxIdxKey];
            
                        int maxIdxPtr = leftNode.getPointer2TreeOrData(null,null).
                            getPointer2InternalNodes().Count - 1;
                        cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().
                            Insert(0, leftNode.getPointer2TreeOrData(null, null).
                                getPointer2InternalNodes()[maxIdxPtr]);
                        //cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()
                        //    .insert(cursor->ptr2TreeOrData.ptr2Tree.begin(), leftNode->ptr2TreeOrData.ptr2Tree[maxIdxPtr]);
            
                        //resize the left Sibling Node After Tranfer
                        //leftNode->keys.resize(maxIdxKey);
                        //leftNode->ptr2TreeOrData.dataPtr.resize(maxIdxPtr);
            
                        return;
                    }
                }
        
                // If possible transfer to rightSibling
                else if (rightSibling < parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count)
                {
                    BPlusTreeNode rightNode = parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[rightSibling];
            
                    //Check if LeftSibling has extra Key to transfer
                    if (rightNode.getAllKeys().Count >= (maxChildLimit + 1) / 2)
                    {
            
                        //transfer key from right sibling through parent
                        int maxIdxKey = rightNode.getAllKeys().Count - 1;
                        cursor.getAllKeys().Add(parent.getAllKeys()[pos]);
                        
                        int temp = rightNode.getKey(0);
                        parent.getAllKeys().RemoveAt(pos);
                        parent.getAllKeys().Insert(pos, temp);
                        rightNode.getAllKeys().RemoveAt(0);
                        
                        //transfer the pointer form rightSibling to cursor
                        cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().
                            Add(rightNode.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[0]);
                        cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().
                            RemoveAt(0);

                        return;
                        // parent.getAllKeys()[pos] = rightNode.getAllKeys()[0];
                        //rightNode.getAllKeys().erase(rightNode.getAllKeys().begin());
            
                        ////transfer the pointer from rightSibling to cursor
                        //cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()
                        //    .Add(rightNode.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[0]);
                        //cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()
                        //    .erase(cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes().begin());
            
                        // return;
                    }
                }
        
                //Start to Merge Now, if None of the above cases applied
                else if (leftSibling >= 0)
                {
                    //leftNode + parent key + cursor
                    BPlusTreeNode leftNode = parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[leftSibling];
                    leftNode.getAllKeys().Add(parent.getAllKeys()[leftSibling]);
            
                    foreach (int val in cursor.getAllKeys())
                    {
                        leftNode.getAllKeys().Add(val);
                    }
            
                    for (int i = 0; i < cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count; i++)
                    {
                        leftNode.getPointer2TreeOrData(null,null).getPointer2InternalNodes()
                            .Add(cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i]);
                        cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().RemoveAt(i);
                        cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Insert(i, null);
                    }
            
                    //cursor->ptr2TreeOrData.ptr2Tree.resize(0);
                    //cursor->keys.resize(0);
                    cursor.getAllKeys().Clear();
                    cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Clear();
            
                    removeInternal(parent.getKey(leftSibling), parent, cursor);
                    Console.WriteLine("Merged with left sibling");
                }
                
                else if (rightSibling < parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count)
                {
                    //cursor + parentkey +rightNode
                    BPlusTreeNode rightNode = parent.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[rightSibling];
                    cursor.getAllKeys().Add(parent.getAllKeys()[rightSibling - 1]);
            
                    foreach (int val in rightNode.getAllKeys())
                    {
                        cursor.getAllKeys().Add(val);
                    }
            
                    for (int i = 0; i < rightNode.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count; i++)
                    {
                        cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()
                            .Add(rightNode.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i]);
                        // rightNode.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i] = null;
                        rightNode.getPointer2TreeOrData(null, null).getPointer2InternalNodes().RemoveAt(i);
                        rightNode.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Insert(i, null);
                    }
            
                    //rightNode->ptr2TreeOrData.ptr2Tree.resize(0);
                    //rightNode->keys.resize(0);
                    rightNode.getAllKeys().Clear();
                    rightNode.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Clear();
            
                    removeInternal(parent.getAllKeys()[rightSibling - 1], parent, rightNode);
                    Console.WriteLine("Merged with right sibling");
                }
            }
        }

        /*
         * function to find range of values stored in B+ Tree
         */
        public void searchRange(int lowerBound, int upperBound)
        {
            Console.WriteLine("Searching for key: {0} ...", lowerBound);
            if (root == null)
            {
                Console.WriteLine("B+ Tree is empty, insert keys first!");
            }
            else
            {
                List<Record> records = new List<Record>();
                BPlusTreeNode cursor = root;
                while (cursor.checkIsLeaf() == false)
                {
                    List<int> temp = cursor.getAllKeys();
                    int index = findIndex(temp, lowerBound);
                    Console.WriteLine("Next index to go to  = " + index);

                    //go to child node
                    cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[index];
                    Console.WriteLine("1st key of next node = " + cursor.getAllKeys()[0]);
                }
                List<int> leafNodes = cursor.getAllKeys();
                int leafNodeIndex = 0;
                for (int i = 0; i < leafNodes.Count; i++)
                {
                    if (lowerBound == leafNodes[i])
                        leafNodeIndex = i;
                }

                //key found
                bool check = false;
                while (cursor.getPointer2Next() != null && check == false)
                {
                    List <Record> found = cursor.getPointer2TreeOrData(null, null).getPointer2Records();
                   
                    foreach(Record r in found)
                    {
                        if (r.getNumVotes() > upperBound)
                        {
                            check = true;
                            break;
                        }
                        records.Add(r);
                        Console.Write("Record details: ");
                        r.printRecord();
                    }
                    
                    cursor = cursor.getPointer2Next();
                }

                double ave = 0;
                int count = 0;
                foreach(Record r in records)
                {
                    ave += r.getAverageRating();
                    count++;
                }

                ave = ave / count;
                Console.WriteLine("Average of average rating = " + ave);
            }
        }
    }
}
    