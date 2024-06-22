using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeConsoleGame
{
    public class Player
    {
        public int PosRow { get; set; }
        public int PosCol { get; set; }
        const int moveTick = 100;
        private int _sumTick = 0;
        private Board? _board;
        private int _destRow;
        private int _destCol;

        public void PlayerInit(int posRow, int posCol, int destRow, int destCol, Board board)
        {
            PosRow = posRow;
            PosCol = posCol;
            _board = board;
            _destRow = destRow;
            _destCol = destCol;
        }

        private Random random = new();

        public void Update(int deltaTick)
        {
            if (_board == null || _board._tile == null) return;

            _sumTick += deltaTick;

            // 0.1 초 마다  player 이동
            if (_sumTick >= moveTick)
            {
                _sumTick = 0;

                int rndValue = random.Next(0, 5);

                switch (rndValue)
                {
                    case 0: // 상
                        if (_board._tile[PosRow - 1, PosCol] == TileType.Empty) PosRow =  PosRow - 1;
                        break;

                    case 1: // 하
                        if (_board._tile[PosRow + 1, PosCol] == TileType.Empty) PosRow = PosRow + 1;
                        break;

                    case 2: // 좌
                        if (_board._tile[PosRow, PosCol - 1] == TileType.Empty) PosCol = PosCol - 1;
                        break;
                    case 3: // 우
                        if (_board._tile[PosRow, PosCol + 1] == TileType.Empty) PosCol = PosCol + 1;
                        break;
                }

            }
        }
    }
}
