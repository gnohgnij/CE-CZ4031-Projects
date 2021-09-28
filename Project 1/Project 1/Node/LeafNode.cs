using System;
using System.Collections.Generic;

namespace Project_1
{
    public struct LeafNode
    {
        private List<Record> pointers;
        private List<int> keys;
        private static int maxNumberOfKeys = 3;
        private int level;

        public LeafNode(List<int> keys, List<Record> pointers, int level)
        {
            this.keys = keys;
            this.pointers = pointers;
            this.level = level;
            
            keys.Sort((k1, k2) => k1.CompareTo(k2));
            pointers.Sort((r1, r2) => r1.getNumVotes().CompareTo(r2.getNumVotes()));
        }
        
        public List<int> getKeys()
        {
            return this.keys;
        }

        public List<Record> getPointers()
        {
            return pointers;
        }

        public int getMaxNumberOfKeys()
        {
            return maxNumberOfKeys;
        }

        public int getLevel()
        {
            return this.level;
        }
        
        public void setLevel(int level)
        {
            this.level = level;
        }
        
        public void printAllKeys()
        {
            foreach (int i in keys)
            {
                Console.WriteLine(i);
            }
        }

        public void printAllRecords()
        {
            foreach (Record r in pointers)
            {
                r.printRecord();
            }
        }

        public void checkNonLeaf()
        {

        }

        public List<int> reOrderNode(List<int> keys)
        {
            keys.Sort((k1, k2) => k1.CompareTo(k2));
            pointers.Sort((r1, r2) => r1.getNumVotes().CompareTo(r2.getNumVotes()));
            return keys;
        }
        public void checkDuplicate()
        {
            //Logic to check for duplicate
        }
        public bool isRoot()
        {
            return true;
        }
        public int floorFunctionLeafNode(float value)
        {
            int floor = (int)Math.Floor((value + 1) / 2);
            return floor;
        }

        public int floorFunctionNonLeafNode(float value)
        {
            int floor = (int)Math.Floor(value / 2);
            return floor;
        }
    }
}