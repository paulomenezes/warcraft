namespace Warcraft.Util
{
    class PathNode
    {
        public int x;
        public int y;

        public int F;
        public int G;
        public int H;

        public PathNode parent;

        public PathNode(int x, int y, int g, int h, PathNode parent)
        {
            this.x = x;
            this.y = y;

            this.F = g + h;
            this.G = g;
            this.H = h;

            this.parent = parent;
        }
    }
}
