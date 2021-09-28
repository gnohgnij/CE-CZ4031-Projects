using System;
using System.Collections.Generic;

namespace Project_1
{
    public struct Delete
    {
        private string input;
        public Delete(string input)
        {
            this.input = input;
            LeafNode leafNode = new LeafNode();
            leafNode.isRoot();
        }
    }
}