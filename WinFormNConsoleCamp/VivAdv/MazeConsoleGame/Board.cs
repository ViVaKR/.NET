using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsoleGame
{
    public class Board
    {
        public Player? _player;
        public TileType[,]? _tile;

        const char circle = '\u25cf';


        public int _size;

        public void InitializeBoard(int size, Player player)
        {
            if (size % 2 == 0) return;

            _player = player;
            _size = size;

            CreateMaze();
        }

        private void CreateMaze()
        {
            _tile = new TileType[_size, _size];

            for (int row = 0; row < _size; row++)
            {
                for (int col = 0; col < _size; col++)
                {
                    _tile[row, col] =
                        (row % 2 == 0 || col % 2 == 0)
                        ? TileType.Wall
                        : TileType.Empty;
                }
            }
            for (int row = 0; row < _size; row++)
            {
                int count = 1;

                for (int col = 0; col < _size; col++)
                {
                    if (row % 2 == 0 || col % 2 == 0) continue;

                    if (row == _size - 2 && col == _size - 2) continue;

                    if (row == _size - 2)
                    {
                        _tile[row, col + 1] = TileType.Empty;
                        continue;
                    }

                    if (col == _size - 2)
                    {
                        _tile[row + 1, col] = TileType.Empty;
                        continue;
                    }

                    var random = new Random();

                    if (random.Next(0, 2) == 0)
                    {
                        _tile[row, col + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int idx = random.Next(0, count);
                        _tile[row + 1, col - idx * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }

        private static ConsoleColor GetTileColor(TileType type)
        {
            return type switch
            {
                TileType.Empty => ConsoleColor.Green,
                TileType.Wall => ConsoleColor.Red,
                _ => ConsoleColor.Green,
            };
        }

        public void Render()
        {
            if (_tile == null || _player == null) return;

            ConsoleColor preColor = Console.ForegroundColor;

            for (int row = 0; row < _size; row++)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                for (int col = 0; col < _size; col++)
                {
                    if (row == _player.PosRow && col == _player.PosCol)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = GetTileColor(_tile[row, col]);
                    }
                    Console.Write($"{circle} ");
                }

                Console.WriteLine();

            }

            Console.ForegroundColor = preColor;
        }
    }


    public enum TileType
    {
        Empty,
        Wall
    }
}
