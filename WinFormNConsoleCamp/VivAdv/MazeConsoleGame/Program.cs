
using MazeConsoleGame;

const int waitTick = 1000 / 30;

Board board = new();
Player player = new();
board.InitializeBoard(25, player);
player.PlayerInit(1, 1, board._size - 2, board._size - 2, board);

Console.CursorVisible = false;

int lastTick = 0;

while (true)
{
    int currentTick = Environment.TickCount;

    // 경과 시간이 1/30 초보다 작다면?
    if (currentTick - lastTick < waitTick) continue;



    lastTick = currentTick;

    // 1) 사용자 입력 대기

    // 2) 입력과 기타 로직 처리
    int deltaTick = currentTick - lastTick;
    player.Update(deltaTick);

    // 3) 렌더링

    Console.SetCursorPosition(0, 0);

    board.Render();
}