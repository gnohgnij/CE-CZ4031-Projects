namespace Project_1
{
    public struct NonLeafNode
    {
        private int index;
        private int pointing;

        public NonLeafNode(int index, int pointing)
        {
            this.index = index;
            this.pointing = pointing;
        }

        public bool isLeaf()
        {
            return false;
        }
    }
}