using System;
using System.Collections.Generic;

namespace Project_1.Node
{
    public class BPTree
    {
        private static int maxChildLimit = 4;
        private static int maxLeafNodeLimit = 3;   //to change
        private BPlusTreeNode root;
        
        public BPlusTreeNode getRoot()
        {
            return this.root;
        }
        
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
                    // int index = -1;
                    // int max = 0;
                    // foreach (int i in temp)
                    // {
                    //     index++;
                    //     if (i >= key)
                    //     {
                    //         max = i;
                    //         break;
                    //     }
                    // }
                    //
                    // if (max >= key)
                    //     index++;
                    
                    //TODO: fix this
                    int index = 0;
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (key < temp[i])
                        {
                            index = i;
                            break;
                        }

                        else if (key == temp[i])
                        {
                            index = i+1;
                            break;
                        }
                        
                        else if (key > temp[i])
                        {
                            continue;
                        }
                    }
                    
                    Console.WriteLine("Leaf node index to traverse to: " + index);
                    
                    //go to child node
                    cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[index];
                }
                temp = cursor.getAllKeys();
                int leafNodeIndex = -1;
                foreach (int i in temp)
                {
                    leafNodeIndex++;
                    if (i == key)
                        break;
                }
                
                // Console.WriteLine("The key should be found at leaf node index: " + leafNodeIndex);
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
                root.getPointer2TreeOrData(new List<BPlusTreeNode>(), new List<Record>())
                    .getPointer2Records().Add(record);
                Console.WriteLine("I AM GROOT!");
                root.getPointer2TreeOrData(new List<BPlusTreeNode>(), new List<Record>())
                    .printAllRecords();
            }
            else
            {
                BPlusTreeNode cursor = root;
                BPlusTreeNode parent = null;

                while (cursor.checkIsLeaf() == false)
                {
                    parent = cursor;
                    int index = -1;
                    int max = -1;
                    List<int> temp = cursor.getAllKeys();
                    Console.WriteLine("Cursor Value"+cursor.getAllKeys()[0]);
                    foreach (int i in temp)
                    {
                        index++;
                        if (i >= key) 
                        {
                            max = i;
                            Console.WriteLine("Max"+max);
                            break;
                        }
                    }
                    if (temp.Count == 1 || max > key)
                        index++;
                    cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[index];
                }
                
                //if node has empty space
                if (cursor.getAllKeys().Count < maxLeafNodeLimit)
                {
                    int index = -1;
                    List<int> temp = cursor.getAllKeys();
                    
                    //find index to insert key
                    foreach (int i in temp)
                    {
                        index++;
                        if (i >= key)
                            break;
                    }
                    
                    //if key to be inserted is > all the keys inside node
                    if (index >= cursor.getAllKeys().Count-1)
                    {
                        cursor.getAllKeys().Add(key);
                        cursor.getPointer2TreeOrData(null, null).getPointer2Records().Add(record);
                    }
                    
                    //shifting of keys
                    else if (index != cursor.getAllKeys().Count - 1)
                    {
                        for (int j = cursor.getAllKeys().Count - 1; j > index; j--)
                        {
                            int k = cursor.getAllKeys()[j-1];
                            Record r = cursor.getPointer2TreeOrData(null, null).getPointer2Records()[j - 1];
                            cursor.getAllKeys().RemoveAt(j-1);
                            cursor.getPointer2TreeOrData(null, null).getPointer2Records().RemoveAt(j-1);
                            cursor.getAllKeys().Insert(j, k);
                            cursor.getPointer2TreeOrData(null, null).getPointer2Records().Insert(j, r);
                        }
                        
                        cursor.getAllKeys().Insert(index, key);
                        cursor.getPointer2TreeOrData(null, null).getPointer2Records().Insert(index, record);
                        Console.WriteLine("Inserted {0} successfully", key);
                        Console.WriteLine("Inserted at index " + index);
                    }
                    
                    cursor.getPointer2TreeOrData(new List<BPlusTreeNode>(), new List<Record>())
                        .printAllRecords();
                }
                else
                {
                    List<int> virtualNode = new List<int>();
                    List<Record> virtualDataNode = new List<Record>();
                        
                    for (int i = 0; i < cursor.getAllKeys().Count; i++)
                    {
                        virtualNode.Add(cursor.getAllKeys()[i]);
                        virtualDataNode.Add(cursor.getPointer2TreeOrData(null,null).getPointer2Records()[i]);
                    }

                    //finding the probable place to insert the key
                    int index = -1;
                    List<int> temp = cursor.getAllKeys();
                    foreach (int i in temp)
                    {
                        index++;
                        if (i >= key)
                            break;
                    }

                    if (index >= virtualNode.Count - 1)
                    {
                        virtualNode.Add(key);
                        virtualDataNode.Add(record);
                    }

                    else if (index != virtualNode.Count - 1)
                    {
                        for (int j = virtualNode.Count - 1; j > index; j--)
                        {
                            int k = virtualNode[j - 1];
                            Record r = virtualDataNode[j - 1];
                            virtualNode.RemoveAt(j - 1);
                            virtualDataNode.RemoveAt(j - 1);
                            virtualNode.Insert(j, k);
                            virtualDataNode.Insert(j, r);
                        }

                        virtualNode.Insert(index, key);
                        virtualDataNode.Insert(index, record);
                    }
                    
                    BPlusTreeNode newLeaf = new BPlusTreeNode(new List<int>());
                    newLeaf.setIsLeaf(true);
                    newLeaf.getPointer2TreeOrData(null, new List<Record>());

                    //swapping the next ptr
                    BPlusTreeNode temporary = cursor.getPointer2Next();
                    cursor.setPointer2Next(newLeaf);
                    newLeaf.setPointer2Next(temporary);
                    
                    //resizing and copying the keys and dataPtr to Oldnode
                    cursor.getAllKeys().Clear();
                    cursor.getPointer2TreeOrData(null, null).getPointer2Records().Clear();
                    
                    for (int i = 0; i < maxLeafNodeLimit / 2 + 1; i++)
                    {
                        // Console.WriteLine(virtualNode[i]);
                        cursor.getAllKeys().Add(virtualNode[i]);
                        cursor.getPointer2TreeOrData(null, null).getPointer2Records().Add(virtualDataNode[i]);
                    }

                    //pushing new keys and records to new leaf node
                    for (int i = maxLeafNodeLimit / 2 + 1; i < virtualNode.Count; i++)
                    {
                        newLeaf.getAllKeys().Add(virtualNode[i]);
                        newLeaf.getPointer2TreeOrData(null, null).getPointer2Records().Add(virtualDataNode[i]);
                    }
                    
                    newLeaf.getPointer2TreeOrData(null, null).printAllRecords();
                    
                    // Console.WriteLine("int count = " + cursor.getAllKeys().Count);

                    if (cursor == root)
                    {
                        BPlusTreeNode newRoot = new BPlusTreeNode(new List<int>());
                        newRoot.getAllKeys().Add(newLeaf.getAllKeys()[0]);
                        newRoot.getPointer2TreeOrData(new List<BPlusTreeNode>(), null).getPointer2InternalNodes()
                            .Add(cursor);
                        newRoot.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Add(newLeaf);
                        root = newRoot;
                        Console.WriteLine("Created New Root");
                        newLeaf.getPointer2TreeOrData(null,null).printAllRecords();
                    }
                    else
                    {
                        insertInternal(newLeaf.getAllKeys()[0], parent, newLeaf);
                    }
                }
            }
        }

        public void insertInternal(int key, BPlusTreeNode cursor, BPlusTreeNode child)
        {
            if (cursor.getAllKeys().Count < maxChildLimit - 1)
            {
                //TODO: for loop thing again
                int index = -1;
                List<int> temp = cursor.getAllKeys();
                    
                //find index to insert key
                foreach (int i in temp)
                {
                    index++;
                    if (i >= key)
                        break;
                }
                
                if (index >= cursor.getAllKeys().Count-1)
                {
                    cursor.getAllKeys().Add(key);
                    cursor.getPointer2TreeOrData(new List<BPlusTreeNode>(), null).
                        getPointer2InternalNodes().Add(child);
                }

                if (index != cursor.getAllKeys().Count - 1)
                {
                    for (int j = cursor.getAllKeys().Count - 1; j > index; j--)
                    {
                        int k = cursor.getAllKeys()[j - 1];
                        cursor.getAllKeys().RemoveAt(j-1);
                        cursor.getAllKeys().Insert(j, k);
                    }
                    
                    for (int j = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Count - 1; 
                        j > index; j--)
                    {
                        BPlusTreeNode k = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[j - 1];
                        cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().RemoveAt(j-1);
                        cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Insert(j, k);
                    }
                    
                    //TODO: dont understand
                    cursor.getAllKeys()[index] = key;
                    cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[index + 1] = child;
                }
                
                Console.WriteLine("Inserted key in the internal node successfully");
            }
            else
            {
                Console.WriteLine("Inserted node in internal node successful");
                Console.WriteLine("Overflow in internal, splitting internal nodes");

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

                int index = -1;
                List<int> temp = cursor.getAllKeys();

                foreach (int i in temp)
                {
                    index++;
                    if (i >= key)
                        break;
                }

                if (index >= cursor.getAllKeys().Count - 1)
                {
                    virtualKeyNode.Add(key);
                    virtualTreePtrNode.Add(child);
                }

                if (index != virtualKeyNode.Count - 1)
                {
                    for (int j = virtualKeyNode.Count - 1; j > index; j--)
                    {
                        int k = virtualKeyNode[j - 1];
                        virtualKeyNode.RemoveAt(j - 1);
                        virtualKeyNode.Insert(j, k);
                    }

                    // TODO: dont understand
                    for (int j = virtualTreePtrNode.Count - 1; j > index + 1; j--)
                    {
                        BPlusTreeNode k = virtualTreePtrNode[j - 1];
                        virtualTreePtrNode.RemoveAt(j - 1);
                        virtualTreePtrNode.Insert(j, k);
                    }

                    virtualKeyNode[index] = key;
                    virtualTreePtrNode[index + 1] = child;
                }

                int partitionKey = virtualKeyNode[virtualKeyNode.Count / 2];
                int partitionIndex = virtualKeyNode.Count / 2;

                cursor.getAllKeys().Clear();
                cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Clear();

                for (int i = 0; i < partitionIndex; i++)
                {
                    cursor.getAllKeys().Add(virtualKeyNode[i]);
                }

                for (int i = 0; i < partitionIndex + 1; i++)
                {
                    cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes().Add(virtualTreePtrNode[i]);
                }

                BPlusTreeNode newInternalNode = new BPlusTreeNode(new List<int>());
                
                //Might have something to add here

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
                    
                    // might need to add something later on
                    
                    newRoot.getPointer2TreeOrData(new List<BPlusTreeNode>(), null).getPointer2InternalNodes().Add(cursor);
                    newRoot.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Add(newInternalNode);
                    newRoot = root;
                    Console.WriteLine("Created new root");
                }
            }
        }
        public void Delete(int x)
        {
            if (root == null)
            {
                Console.WriteLine("B+ Tree is empty.");
                return;
            }

            BPlusTreeNode cursor = root;
            BPlusTreeNode parent = null;
            int leftSibling = 0, rightSibling = 0;

            while (cursor.checkIsLeaf() != true)
            {
                for (int i = 0; i < cursor.getAllKeys().Count; i++)
                {
                    parent = cursor;
                    leftSibling = i - 1;
                    rightSibling = i + 1;

                    if (x < cursor.getAllKeys()[i])
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
                if (cursor.getAllKeys()[pos] == x)
                {
                    found = true;
                    break;
                }
            }

            //  var itr = lower_bound
            var itr = 0;

            //if (itr == cursor->keys.end())
            //{
            //    cout << "Key Not Found in the Tree" << endl;
            //    return;
            //}

            //// Delete the respective File and FILE*
            //string fileName = "DBFiles/" + to_string(x) + ".txt";
            //char filePtr[256];
            //strcpy(filePtr, fileName.c_str());

            ////delete cursor->ptr2TreeOrData.dataPtr[pos];//avoid memory leaks
            //if (remove(filePtr) == 0)
            //    cout << "SuccessFully Deleted file: " << fileName << endl;
            //else
            //    cout << "Unable to delete the file: " << fileName << endl;

            for (int i = pos; i < cursor.getAllKeys().Count - 1; i++)
            {
                cursor.getAllKeys()[i] = cursor.getAllKeys()[i + 1];
                cursor.getPointer2TreeOrData(null, null).getPointer2Records()[i] = cursor.getPointer2TreeOrData(null, null).getPointer2Records()[i + 1];
            }
            int prev_size = cursor.getAllKeys().Count;
            //cursor->keys.resize(prev_size - 1);
            //cursor->ptr2TreeOrData.dataPtr.resize(prev_size - 1);

            if (cursor == root)
            {
                if (cursor.getAllKeys().Count == 0)
                {
                    // setRoot(null);
                    Console.Write("Empt Tree.");
                }
            }

            Console.WriteLine("Deleted" + x + "From Leaf Node successfull");
            if (cursor.getAllKeys().Count >= (maxLeafNodeLimit + 1) / 2 + 1)
            {
                return;
            }
            Console.WriteLine("Underflow in the leaf node happened");
            Console.WriteLine("Starting redistribution");

            if (leftSibling >= 0)
            {
                BPlusTreeNode leftNode = parent.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[leftSibling];
                
                if (leftNode.getAllKeys().Count >= (maxLeafNodeLimit + 1) / 2 + 1)
                {
                    //int maxIndex = leftNode.getAllKeys().Count - 1;
                    //cursor->keys.insert(cursor->keys.begin(), leftNode->keys[maxIdx]);
                    //cursor->ptr2TreeOrData.dataPtr
                    //    .insert(cursor->ptr2TreeOrData.dataPtr.begin(), leftNode->ptr2TreeOrData.dataPtr[maxIdx]);

                    ////resize the left Sibling Node After Tranfer
                    //leftNode->keys.resize(maxIdx);
                    //leftNode->ptr2TreeOrData.dataPtr.resize(maxIdx);

                    parent.getAllKeys()[leftSibling] = cursor.getAllKeys()[0];
                    Console.WriteLine("Transferred from left sibling of leaf node");
                    return;
                }       
            }

            if (rightSibling < parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count)
            {
                BPlusTreeNode rightNode = parent.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[rightSibling];
                if (rightNode.getAllKeys().Count >= (maxLeafNodeLimit + 1) / 2 + 1)
                {
                    int minIndex = 0;
                    cursor.getAllKeys().Add(rightNode.getAllKeys()[minIndex]);
                    cursor.getPointer2TreeOrData(null, null).getPointer2Records().Add(rightNode.getPointer2TreeOrData(null, null).getPointer2Records()[minIndex]);

                    //resize the right Sibling Node After Tranfer
                    //rightNode->keys.erase(rightNode->keys.begin());
                    //rightNode->ptr2TreeOrData.dataPtr.erase(rightNode->ptr2TreeOrData.dataPtr.begin());//resize the right Sibling Node After Tranfer
                    //rightNode->keys.erase(rightNode->keys.begin());
                    //rightNode->ptr2TreeOrData.dataPtr.erase(rightNode->ptr2TreeOrData.dataPtr.begin());

                    parent.getAllKeys()[rightSibling - 1] = rightNode.getAllKeys()[0];
                    Console.WriteLine("Transferred from right sibling of lead node");
                    return;
                }
            }
            if (leftSibling >= 0)
            {
                BPlusTreeNode leftNode = parent.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[leftSibling];

                for (int i = 0; i < cursor.getAllKeys().Count; i++)
                {
                    leftNode.getAllKeys().Add(cursor.getAllKeys()[i]);
                    leftNode.getPointer2TreeOrData(null, null).getPointer2Records().Add(cursor.getPointer2TreeOrData(null, null).getPointer2Records()[i]);
                }

                leftNode.setPointer2Next(cursor.getPointer2Next());
                Console.WriteLine("Merging two leaf nodes");
                //removeInternal(parent.getAllKeys()[rightSibling-1], parent, rightNode);
            }
        }

        public void removeInternal(int key, BPlusTreeNode cursor, BPlusTreeNode child)
        {
            BPlusTreeNode root = getRoot();

            if(cursor == root)
            {
                if(cursor.getAllKeys().Count == 1)
                {
                    if(cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[1] == child)
                    {
                        //setRoot(cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[0]);
                        Console.WriteLine("New Changed Root");
                        return;
                    }
                    else if(cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[0] == child)
                    {
                        //setRoot(cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[1]);
                        Console.WriteLine("New Changed Root");
                        return;
                    }
                }
            }

            // Deleting key x from the parent
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
                cursor.getAllKeys()[i] = cursor.getAllKeys()[i + 1];
            }
            // cursor.getAllKeys().resize(cursor.getAllKeys().Count - 1);

            // Now deleting the ptr2tree
            for (pos = 0; pos < cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count; pos++)
            {
                if (cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[pos] == child)
                {
                    break;
                }
            }

            for (int i = pos; i < cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count - 1; i++)
            {
                cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[i] = cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[i + 1];
            }
            //cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()
            //    .resize(cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count - 1);

            // If there is No underflow. Phew!!
            if (cursor.getAllKeys().Count >= (maxChildLimit + 1) / 2 - 1)
            {
                Console.WriteLine("Deleted " + key + "from internal node succesfull \n");
                return;
            }

            Console.WriteLine("UnderFlow in internal Node.");

            if (cursor == root)
            {
                return;
            }

            //BPlusTreeNode p1 = findParent(root, cursor);
            //BPlusTreeNode parent = p1;
            BPlusTreeNode parent = null;

            int leftSibling = 0, rightSibling = 0;

            // Finding Left and Right Siblings as we did earlier
            for (pos = 0; pos < parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count; pos++)
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
                    //cursor->keys.insert(cursor->keys.begin(), parent->keys[leftSibling]);
                    parent.getAllKeys()[leftSibling] = leftNode.getAllKeys()[maxIdxKey];

                    int maxIdxPtr = leftNode.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count - 1;
                    //cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()
                    //    .insert(cursor->ptr2TreeOrData.ptr2Tree.begin(), leftNode->ptr2TreeOrData.ptr2Tree[maxIdxPtr]);

                    //resize the left Sibling Node After Tranfer
                    //leftNode->keys.resize(maxIdxKey);
                    //leftNode->ptr2TreeOrData.dataPtr.resize(maxIdxPtr);

                    return;
                }
            }

            // If possible transfer to rightSibling
            if (rightSibling < parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes().Count)
            {
                BPlusTreeNode rightNode = parent.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[rightSibling];

                //Check if LeftSibling has extra Key to transfer
                if (rightNode.getAllKeys().Count >= (maxChildLimit + 1) / 2)
                {

                    //transfer key from right sibling through parent
                    int maxIdxKey = rightNode.getAllKeys().Count - 1;
                    cursor.getAllKeys().Add(parent.getAllKeys()[pos]);
                    parent.getAllKeys()[pos] = rightNode.getAllKeys()[0];
                    //rightNode.getAllKeys().erase(rightNode.getAllKeys().begin());

                    ////transfer the pointer from rightSibling to cursor
                    //cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()
                    //    .Add(rightNode.getPointer2TreeOrData(null,null).getPointer2InternalNodes()[0]);
                    //cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes()
                    //    .erase(cursor.getPointer2TreeOrData(null,null).getPointer2InternalNodes().begin());

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
                    cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i] = null;
                }

                //cursor->ptr2TreeOrData.ptr2Tree.resize(0);
                //cursor->keys.resize(0);

                removeInternal(parent.getAllKeys()[leftSibling], parent, cursor);
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
                    rightNode.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i] = null;
                }

                //rightNode->ptr2TreeOrData.ptr2Tree.resize(0);
                //rightNode->keys.resize(0);

                removeInternal(parent.getAllKeys()[rightSibling - 1], parent, rightNode);
                Console.WriteLine("Merged with right sibling\n");
            }
        }
    }
    }
    
    
}