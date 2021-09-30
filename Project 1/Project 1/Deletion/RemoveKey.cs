//using Project_1.Node;
//using System;
//using System.Collections.Generic;

//namespace Project_1
//{
//    public struct RemoveKey
//    {

//        private int x;
//        BPlusTreeNode root = getRoot(); //From util function
//        public RemoveKey(int x, BPlusTreeNode root)
//        {
//            this.root = root;
//            this.x = x;
//            if(root == null)
//            {
//                Console.WriteLine("B+ Tree is empty.");
//                return;
//            }

//            BPlusTreeNode cursor = root;
//            BPlusTreeNode parent;
//            int leftSibling, rightSibling;

//            while(cursor.checkIsLeaf() != true)
//            {
//                for(int i = 0;  i < cursor.getAllKeys().Count; i++)
//                {
//                    parent = cursor;
//                    leftSibling = i - 1;
//                    rightSibling = i + 1;

//                    if (x < cursor.getAllKeys()[i])
//                    {
//                        cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i];
//                        break;
//                    }
//                    if(i == cursor.getAllKeys().Count - 1)
//                    {
//                        leftSibling = i;
//                        rightSibling = i + 2;
//                        cursor = cursor.getPointer2TreeOrData(null, null).getPointer2InternalNodes()[i + 1];
//                        break;
//                    }
//                }
//            }

//            // Check if the value exists in this leaf node
//            int pos = 0;
//            bool found = false;
//            for(pos = 0; pos< cursor.getAllKeys().Count; pos++)
//            {
//                if(cursor.getAllKeys()[pos] == x)
//                {
//                    found = true;
//                    break;
//                }
//            }

//            //  var itr = lower_bound
//            var itr = 0;

//            //if (itr == cursor->keys.end())
//            //{
//            //    cout << "Key Not Found in the Tree" << endl;
//            //    return;
//            //}

//            //// Delete the respective File and FILE*
//            //string fileName = "DBFiles/" + to_string(x) + ".txt";
//            //char filePtr[256];
//            //strcpy(filePtr, fileName.c_str());

//            ////delete cursor->ptr2TreeOrData.dataPtr[pos];//avoid memory leaks
//            //if (remove(filePtr) == 0)
//            //    cout << "SuccessFully Deleted file: " << fileName << endl;
//            //else
//            //    cout << "Unable to delete the file: " << fileName << endl;

//            for(int i = pos; i < cursor.getAllKeys().Count - 1; i++)
//            {
//                cursor.getAllKeys()[i] = cursor.getAllKeys()[i + 1];
//                cursor.getPointer2TreeOrData(null,null).getPointer2Records()[i] = cursor.getPointer2TreeOrData(null, null).getPointer2Records()[i+1];
//            }
//            int prev_size = cursor.getAllKeys().Count;
//            //cursor->keys.resize(prev_size - 1);
//            //cursor->ptr2TreeOrData.dataPtr.resize(prev_size - 1);

//            if(cursor == root)
//            {
//                if (cursor.getAllKeys().Count == 0)
//                {
//                    // setRoot(null);
//                    Console.Write("Empt Tree.");
//                }
//            }

//            Console.WriteLine("Deleted" + x + "From Leaf Node successfull");
//            if(cursor.getAllKeys().Count >= (maxLeafNodeLimit() + 1) /2 + 1)
//            {
//                return;
//            }
//        }
//    }

//    //private void LowerBound()
//    //{
//    //    int index = -1;
//    //    List<int> temp = cursor.getAllKeys();
//    //    foreach (int i in temp)
//    //    {
//    //        index++;
//    //        if (i <= key)
//    //            break;
//    //    }
//    //}
//}