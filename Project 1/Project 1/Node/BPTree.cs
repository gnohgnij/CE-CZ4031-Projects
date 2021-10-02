using System;
using System.Collections.Generic;

namespace Project_1.Node
{
    public class BPTree
    {
        private int maxChildLimit;
        private int maxLeafNodeLimit;
        private BPlusTreeNode root;
        
        /*
         * function to get maxLeafNodeLimit
         */
        public int getMaxLeafNodeLimit()
        {
            return this.maxLeafNodeLimit;
        }
        
        /*
         * function to get maxChildLimit
         */
        public int getMaxChildLimit()
        {
            return this.maxChildLimit;
        }
        
        /*
         * function to set maxChildLimit
         */
        public void setMaxChildLimit(int blocksize)
        {
            this.maxChildLimit = (blocksize-8)/12 + 1;
        }
        
        /*
         * function to set maxLeafNodeLimit
         */
        public void setMaxLeafNodeLimit(int blocksize)
        {
            this.maxLeafNodeLimit = (blocksize-8)/12;
        }
        
        /*
         * function to get content of root node and its 1st child
         */
        public void getRootContent()
        {
            if (root == null)
            {
                Console.WriteLine("B+ tree is empty");
            }
            else
            {
                Console.Write("Content of root node: ");
                foreach (int i in root.getAllKeys())
                {
                    Console.Write("{0}, ", i);
                }
                Console.WriteLine();
                Console.Write("Content of 1st child of root node: ");
                foreach (int i in 
                    root.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[0].getAllKeys())
                {
                    Console.Write("{0}, ", i);
                }
                Console.WriteLine();
            }
        }
        
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
        public bool search(int key)
        {
            int height = 0;
            Console.WriteLine("Searching for key {0}...", key);
            if (root == null)
            {
                Console.WriteLine("B+ Tree is empty, insert keys first!");
                Console.WriteLine("Height of tree = {0}", height);
                return false;
            }
            else
            {
                height++;
                int numOfNodesAccessed = 0;
                BPlusTreeNode cursor = root;
                while (cursor.checkIsLeaf() == false)
                {
                    List<int> temp = cursor.getAllKeys();
                    int index = findIndex(temp, key);
                    
                    //go to child node
                    cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[index];
                    numOfNodesAccessed++;
                    height++;
                    Console.WriteLine("1st key of next node = " + cursor.getAllKeys()[0]);
                }
                List<int> leafNodes = cursor.getAllKeys();
                int leafNodeIndex = 0;
                for (int i = 0; i < leafNodes.Count; i++)
                {
                    if (key == leafNodes[i])
                    {
                        leafNodeIndex = i;
                        break;
                    }
                }
                
                //key not found
                if (leafNodes[leafNodeIndex] != key)
                {
                    Console.WriteLine("Key not found");
                    return false;
                }
                
                //key found
                else
                {
                    Console.WriteLine("Key found!");
                    Console.WriteLine("Record details:");
                    Console.WriteLine("-------------------------------------------");
                    int recordKey = leafNodes[leafNodeIndex];
                    while (key == recordKey)
                    {
                        Record found = cursor.getPointer2TreeOrData(null, null).getPointer2Records()[leafNodeIndex];
                        found.printRecord();
                        leafNodeIndex++;
                        if (leafNodeIndex == cursor.getPointer2TreeOrData(null, null).getPointer2Records().Count)
                        {
                            cursor = cursor.getPointer2Next();
                            leafNodeIndex = 0;
                            numOfNodesAccessed++;
                        }
                        recordKey = cursor.getPointer2TreeOrData(null, null).getPointer2Records()[leafNodeIndex]
                            .getNumVotes();
                    }
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("Total number of index nodes accessed = {0}", numOfNodesAccessed);
                    Console.WriteLine("Total height of the B+ tree = {0}", height);
                    return true;
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
                if (cursor.getAllKeys().Count < getMaxLeafNodeLimit())
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
                    
                    //swapping pointers
                    BPlusTreeNode temporary = cursor.getPointer2Next(); 
                    cursor.setPointer2Next(newLeaf); 
                    newLeaf.setPointer2Next(temporary);

                    //put first n/2 + 1 keys into old leaf node
                    cursor.getAllKeys().Clear();
                    cursor.getPointer2TreeOrData(null, null).getPointer2Records().Clear();
                    for (int i = 0; i < getMaxLeafNodeLimit() / 2 + 1; i++)
                    {
                        cursor.getAllKeys().Add(virtualNode[i]);
                        cursor.getPointer2TreeOrData(null, null).getPointer2Records().Add(virtualDataNode[i]);
                    }

                    //put remaining keys into new leaf node
                    for (int i = getMaxLeafNodeLimit() / 2 + 1; i < virtualNode.Count; i++)
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
            if (cursor.getAllKeys().Count < getMaxChildLimit() - 1)
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
        public void delete(int key)
        {
            BPlusTreeNode root = getRoot();
            
            //if root is empty, cannot delete anything
            if (root == null)
            {
                Console.WriteLine("B+ Tree is empty.");
                return;
            }
        
            BPlusTreeNode cursor = root;
            BPlusTreeNode parent = null;
            int leftSibling = -1, rightSibling = -1;
        
            //traversing to the leaf node that may contain the key
            while (cursor.checkIsLeaf() != true)
            {
                for (int i = 0; i < cursor.getAllKeys().Count; i++)
                {
                    parent = cursor;
                    leftSibling = i - 1;
                    rightSibling = i + 1;
                    
                    //if key < element in node, go to first child node
                    if (key < cursor.getAllKeys()[i])
                    {

                        cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i];
                        break;
                    }
                    
                    //if key >= all elements in node, go to last child node
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
                if (cursor.getAllKeys()[pos] == key)
                {
                    found = true;
                    Console.WriteLine("Key {0} found, deleting...", key);
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Key {0} not found", key);
                return;
            }
        
            //  var itr = lower_bound
            // int itr = 0;
            // List<int> temp = cursor.getAllKeys();
            //
            // foreach (int i in temp)
            // {
            //     if (key <= i)
            //         break;
            //     //if key > current i : run another iteration
            //     itr++;
            // }
            // if (key > temp[temp.Count-1])
            // {
            //     itr++;
            // }
            //
            // if (itr == cursor.getAllKeys().Count)
            // {
            //     Console.WriteLine("Key not found!");
            //     return;
            // }

            // Delete the respective key and record
            cursor.getAllKeys().RemoveAt(pos);    //remove key
            cursor.getPointer2TreeOrData(null, null).getPointer2Records().RemoveAt(pos);

            //if leaf node is root node
            if (cursor == root)
            {
                //if no more keys in root node
                if (cursor.getAllKeys().Count == 0)
                {
                    setRoot(null);
                    Console.Write("B+ Tree is now empty...");
                }
            }
            Console.WriteLine("Deleted {0} from leaf node successfully!", key);
            
            //each leaf node should have floor function of (n+1)/2 keys
            if (cursor.getAllKeys().Count >= (getMaxLeafNodeLimit() + 1) / 2)    
            {
                return;
            }
            Console.WriteLine("Underflow in the leaf node happened");
            Console.WriteLine("Starting redistribution...");
            
            //try borrowing key from left sibling node
            if (leftSibling >= 0)
            {
                BPlusTreeNode leftNode = parent.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[leftSibling];
                
                //if left sibling node has enough keys to lend, [(n+1)/2]+1 keys
                if (leftNode.getAllKeys().Count >= (getMaxLeafNodeLimit() + 1) / 2 + 1)
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
                }       
            }
            
            //try to borrow key from right sibling node
            else if (rightSibling < parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count)
            {
                BPlusTreeNode rightNode = parent.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[rightSibling];
                
                //if right sibling node has enough keys to lend, [(n+1)/2]+1 keys
                if (rightNode.getAllKeys().Count >= (getMaxLeafNodeLimit() + 1) / 2 + 1)
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
            if (leftSibling >= 0)
            {
                Console.WriteLine("Unable to borrow keys from sibling nodes, merging with left sibling...");
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
                Console.WriteLine("Merging with left sibling successful");
                removeInternal(parent.getKey(leftSibling), parent, cursor);
            }
                
            else if (rightSibling <= parent.getAllKeys().Count)
            { 
                Console.WriteLine("Unable to borrow keys from sibling nodes, merging with right sibling...");
                BPlusTreeNode rightNode = parent.getPointer2TreeOrData(null, null).
                    getPointer2InternalNodes()[rightSibling];
                    
                //transfer key and record to right sibling node and connect pointer2next;
                for (int i = 0; i < rightNode.getAllKeys().Count; i++)
                {
                    cursor.getAllKeys().Add(rightNode.getKey(i));
                    cursor.getPointer2TreeOrData(null, null).getPointer2Records().
                        Add(rightNode.getPointer2TreeOrData(null, null).getPointer2Records()[i]);
                }
                cursor.setPointer2Next(rightNode.getPointer2Next());
                Console.WriteLine("Merging with right sibling successful");
                removeInternal(parent.getKey(rightSibling-1), parent, rightNode); 
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
            cursor.getAllKeys().RemoveAt(pos);
            // for (int i = pos; i < cursor.getAllKeys().Count - 1; i++)
            // {
            //     int k = cursor.getKey(i + 1);
            //     cursor.getAllKeys().RemoveAt(i);
            //     cursor.getAllKeys().Insert(i, k);
            // }
        
            //Deleting the pointer2tree
            for (pos = 0; pos < cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count; pos++)
            {
                if (cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[pos] == child)
                {
                    break;
                }
            }
            cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().RemoveAt(pos);
            // for (int i = pos; i < cursor.getPointer2TreeOrData(null,null).
            //     getPointer2InternalNodes().Count - 1; i++)
            // {
            //     BPlusTreeNode k = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i + 1];
            //     cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().RemoveAt(i);
            //     cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Insert(i, k);
            // }
        
            //if no underflow
            if (cursor.getAllKeys().Count >= (getMaxChildLimit() + 1) / 2 - 1)
            {
                Console.WriteLine("Deleted {0} from internal node succesfully", key);
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
        
                // If possible to transfer to leftSibling
                if (leftSibling >= 0)
                {
                    BPlusTreeNode leftNode = parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[leftSibling];
            
                    //Check if LeftSibling has extra Key to transfer
                    if (leftNode.getAllKeys().Count >= (getMaxChildLimit() + 1) / 2)
                    {
                        //transfer key from left sibling through parent
                        int maxIdxKey = leftNode.getAllKeys().Count - 1;
                        cursor.getAllKeys().Insert(0, parent.getKey(leftSibling));
                        int temp = leftNode.getKey(maxIdxKey);
                        parent.getAllKeys().RemoveAt(leftSibling);
                        parent.getAllKeys().Insert(leftSibling, temp);
            
                        int maxIdxPtr = leftNode.getPointer2TreeOrData(null,null).
                            getPointer2InternalNodes().Count - 1;
                        cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().
                            Insert(0, leftNode.getPointer2TreeOrData(null, null).
                                getPointer2InternalNodes()[maxIdxPtr]);
                        Console.WriteLine("cursor" + cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[0]);
                        return;
                    }
                }
        
                // If possible to transfer to rightSibling
                if (rightSibling < parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count)
                {
                    BPlusTreeNode rightNode = parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[rightSibling];
            
                    //Check if RightSibling has extra Key to transfer
                    if (rightNode.getAllKeys().Count >= (getMaxChildLimit() + 1) / 2)
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
                    }
                }

                //Start to Merge Now, if None of the above cases applied
                if (leftSibling >= 0)
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
                    cursor.getAllKeys().Clear();
                    cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Clear();
            
                    removeInternal(parent.getKey(leftSibling), parent, cursor);
                    Console.WriteLine("Merged with left sibling");
                }
                
                else if (rightSibling < parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count)
                {
                    //cursor + parentkey + rightNode
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
                        rightNode.getPointer2TreeOrData(null, null).getPointer2InternalNodes().RemoveAt(i);
                        rightNode.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Insert(i, null);
                    }
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
            lowerBound--;
            int numOfNodesAccessed = 1;
            if (root == null)
            {
                Console.WriteLine("B+ Tree is empty, insert keys first!");
                Console.WriteLine("Number of index nodes accessed = {0}", numOfNodesAccessed-1);
            }
            else
            {
                List<Record> records = new List<Record>();
                BPlusTreeNode cursor = root;
                while (cursor.checkIsLeaf() == false)
                {
                    List<int> temp = cursor.getAllKeys();
                    int index = findIndex(temp, lowerBound);

                    //go to child node
                    cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[index];
                    numOfNodesAccessed++;
                }
                List<int> leafNodes = cursor.getAllKeys();
                int leafNodeIndex = 0;
                for (int i = 0; i < leafNodes.Count; i++)
                {
                    if (lowerBound == leafNodes[i])
                    {
                        leafNodeIndex = i;
                        break;
                    }
                }

                //key found
                bool check = false;
                Console.WriteLine("Searching in process... record details:");
                Console.WriteLine("---------------------------------------");
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
                        
                        if (r.getNumVotes() > lowerBound)
                        {
                            records.Add(r);
                            r.printRecord();
                        }
                    }
                    cursor = cursor.getPointer2Next();
                    numOfNodesAccessed++;
                }
                Console.WriteLine("---------------------------------------");

                double ave = 0;
                int count = 0;
                foreach(Record r in records)
                {
                    ave += r.getAverageRating();
                    count++;
                }

                ave = ave / count;
                Console.WriteLine("Average of average rating = " + ave);
                
                Console.WriteLine("Number of index nodes accessed = {0}", numOfNodesAccessed);
            }
        }
    }
}