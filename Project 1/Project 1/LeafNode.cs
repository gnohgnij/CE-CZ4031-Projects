namespace Project_1
{
    public struct LeafNode
    {
        private int index;
        private LeafNode next;

        public LeafNode(int index, int pointer)
        {
            this.index = index;
            this.pointer = pointer;
        }

        public bool isLeaf()
        {
            return true;
        }

        public int getIndex()
        {
            return index;
        }

        public int getPointer()
        {
            return pointer;
        }
    }
}