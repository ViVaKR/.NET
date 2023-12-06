
using System.Diagnostics;

namespace MazeGame
{
    public class Maze
    {
        public int _count;
        private readonly bool[,] visited;
        public Point current;
        public int[] playerPositions = [1, 1];
        private readonly Point[] fourDirections;
        public readonly int[,] map;
        public List<Rectangle> locations;

        private double wallSize;
        public double WallSize
        {
            get => wallSize;
            set
            {
                wallSize = value;
                current.X = Convert.ToInt32(wallSize);
                current.Y = Convert.ToInt32(wallSize);
            }
        }

        public Maze(int count, int size)
        {
            locations = [];

            _count = count;

            WallSize = size / count;

            map = new int[_count, _count];

            visited = new bool[_count, _count];

            fourDirections = [new Point(1, 0), new Point(0, 1), new Point(-1, 0), new Point(0, -1)];

            InitiallizeMaze();
        }

        private void InitiallizeMaze()
        {
            if (visited == null) return;
            for (int col = 0; col < _count - 1; col++)
            {
                for (int row = 0; row < _count - 1; row++)
                {
                    visited[col, row] = false;
                    map[col, row] = 1;
                }
            }
        }
        private static List<T> ShuffleList<T>(List<T> list)
        {
            Random random = new();
            for (int i = 0; i < list.Count; i++)
            {
                int j = random.Next(i, list.Count);
                (list[j], list[i]) = (list[i], list[j]);
            }
            return list;
        }

        private bool ToBeOrNotToBeThatIsQnQ(int nextCol, int nextRow)
        {
            return
            (nextCol >= 0 && nextCol < _count - 1) &&
            (nextRow >= 0 && nextRow < _count - 1) &&
            (visited != null && !visited[nextCol, nextRow]);
        }

        private void GenerateMaze(int col, int row)
        {
            if (visited == null || map == null) return;

            visited[col, row] = true;
            map[col, row] = 0;

            List<int> directions = ShuffleList(new List<int> { 0, 1, 2, 3 });

            foreach (int dir in directions)
            {
                int nextCol = col + fourDirections[dir].X * 2;
                int nextRow = row + fourDirections[dir].Y * 2;
                if (ToBeOrNotToBeThatIsQnQ(nextCol, nextRow))
                {
                    var dx = col + fourDirections[dir].X;
                    var dy = row + fourDirections[dir].Y;
                    map[dx, dy] = 0;
                    GenerateMaze(nextCol, nextRow);
                }
            }
        }

        public async Task RunAsync() => await Task.Run(() => GenerateMaze(1, 1));

    }
}
