using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 2차원 배열 그리드
    /// </summary>
    public class GameGrid(int rows, int columns)
    {
        private readonly int[,] grid = new int[rows, columns];
        public int Rows { get; set; } = rows;
        public int Columns { get; set; } = columns;

        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }

        public bool IsRowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsRowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void ClearRow(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }

        private void MoveRowDown(int r, int numRows)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }

        public int ClearFullRows()
        {
            int cleared = 0;
            for (int r = Rows - 1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if(cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }

            return cleared;
        }
    }
}


// 7 Blocks
// (1) I-Block
// (2) J-Block
// (3) L-Block
// (4) O-Block
// (5) S-Block
// (6) T-Block
// (7) Z-Block


// State 4
// 0, 90, 180, 270

// Row Offset = 4
// Column Offset = 2