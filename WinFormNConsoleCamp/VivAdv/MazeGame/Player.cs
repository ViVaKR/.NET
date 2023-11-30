
using System.Drawing.Drawing2D;

namespace MazeGame
{
    public class Player
    {
        public Point Location { get; set; }
        public int WallSize { get; set; }

        public Player(Point location, int wallSize)
        {
            Location = location;
            WallSize = wallSize;
        }
    }
}
