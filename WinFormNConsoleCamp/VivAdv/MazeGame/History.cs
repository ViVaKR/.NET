
namespace MazeGame
{
    public class History(int id, Point point, int col, int row)
    {
        public int Id { get; set; } = id;
        public Point Point { get; set; } = point;
        public int Col { get; set; } = col;
        public int Row { get; set; } = row;
    }
}
