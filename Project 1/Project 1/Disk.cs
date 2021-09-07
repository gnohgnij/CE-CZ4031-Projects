using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Project_1
{
    public struct Disk
    {
        private List<Block> blocks;

        public Disk(List<Block> blocks) 
        {
            this.blocks = blocks;
        }

        public void addNewBlock(Block block)
        { 
            blocks.Add(block);
        }

        public int noOfBlocks() 
        {
            return blocks.Count;
        }
    }
}