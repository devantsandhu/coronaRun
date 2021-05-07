namespace first.ast
{
    // Represents a coordinate in 2D grid space
    public class Coord
    {
        public int x;
        public int y;
        
        public Coord(int xCord, int yCord)
        {
            this.x = xCord;
            this.y = yCord;
        }

        public int GetXCord()
        {
            return this.x;
        }
        
        public int GetYCord()
        {
            return this.y;
        }

        public bool equals(Coord other)
        {
            return x == other.x && y == other.y;
        }
    }
}