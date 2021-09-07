using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace Project_1
{
    public struct Disk
    {
        private List<Block> blocks;

        public Disk(List<Block> blocks) 
        {
            this.blocks = blocks;

            if (blocks.Count > 1)
            {
                // sort the blocks based on their smallest tconst in ascending order
                blocks.Sort((b1, b2) => b1.getSmallestTConst().CompareTo(b2.getSmallestTConst())); 
            }
        }

        public void addNewBlock(Block block)
        { 
            blocks.Add(block);
            
            if (blocks.Count > 1)
            {
                // sort the blocks based on their smallest tconst in ascending order
                blocks.Sort((b1, b2) => b1.getSmallestTConst().CompareTo(b2.getSmallestTConst())); 
            }
        }

        public int getNumberOfBlocks() 
        {
            return blocks.Count;
        }

        public List<Block> getBlocks()
        {
            foreach (var block in this.blocks)
            {
                Console.WriteLine("tconst = " + block.getSmallestTConst());
            }

            return this.blocks;
        }
    }
}