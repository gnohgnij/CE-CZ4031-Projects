using System;
using System.Collections.Generic;

namespace Project_1
{
    public struct LeafNode
    {
        // private SortedDictionary<int, Record> map;
        // private static int maxNumberOfKeys = 3;
        //
        // public LeafNode(SortedDictionary<int, Record> map)
        // {
        //     this.map = map;
        // }
        //
        // public SortedDictionary<int, Record>.KeyCollection getKeys()
        // {
        //     return map.Keys;
        // }
        //
        // public SortedDictionary<int, Record>.ValueCollection getPointers()
        // {
        //     return map.Values;
        // }
        //
        // public SortedDictionary<int, Record> getMap()
        // {
        //     return map;
        // }
        //
        // public int getMaxNumberOfKeys()
        // {
        //     return maxNumberOfKeys;
        // }
        //
        // public void printKeys()
        // {
        //     SortedDictionary<int, Record>.KeyCollection temp = getKeys();
        //     foreach (KeyValuePair<int, Record> pair in map)
        //     {
        //         Console.WriteLine("Key: {0} and Value: {1}", pair.Key, pair.Value);
        //     }
        // }

        private List<Record> pointers;
        private List<int> keys;
        private static int maxNumberOfKeys = 3;

        public LeafNode(List<int> keys, List<Record> pointers)
        {
            this.keys = keys;
            this.pointers = pointers;
            
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
    }
}