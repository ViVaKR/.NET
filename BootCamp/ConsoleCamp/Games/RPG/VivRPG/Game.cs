namespace VivRPG;

public class Game
{
    // Variable
    private bool end;

    // Constructors and Destructors
    public Game()
    {
        // -4 / 5 = -.8 
        // 3 / 5 = .6;

        Console.WriteLine("Hello from the Game Class");
    }

    public void Run()
    {
        // while (!end)
        // {
        //     Console.WriteLine("Write a number: ");
        //     int number = Convert.ToInt32(Console.ReadLine());
        //     if(number < 0)
        //     end = true;
        //     else
        //     Console.WriteLine($"You inputed {number}");
        // }
        // Console.WriteLine("Ending Game...");

        // 16 + 25 = 41 
        // 4^2 + 5^2 = 16 + 25
        // -.8^2 + .6^2 = 1
        Console.WriteLine(Math.Sqrt(Math.Pow(-.8, 2) + Math.Pow(0.6, 2)));
        Console.WriteLine(Math.Pow(-.8, 2));
        Console.WriteLine(Math.Pow(-.6, 2));
        Console.WriteLine(.6 / .8);
        Console.WriteLine(4f / 5f);
        Console.WriteLine(Math.Sqrt(41)); // 6.4

        // 대각선으로 움질일때 더 빨리 움직이게 됨 
        // 백터를 정규화 하여야 함
        // 
        Console.WriteLine(Math.Sqrt(Math.Pow(-3, 2) + Math.Pow(3, 2)) / Math.Sqrt(4.24)); // 4.24
        Console.WriteLine(3f / Math.Sqrt(3)); // 4.24

        // 좌표 : x = 1, y = 1 의 빗변의 길이는, 1을 루트 2로 나누어 정규화 함.
        Console.WriteLine(1.0f / Math.Sqrt(2)); // 0.71

        // 좌표 : x = 4, y = 5 의 방향은 
        float x = 4f;
        float y = 5f;
        var xy = x / y;
        Console.WriteLine($"{x}/{y} = {x / y}");

        // 빗변의 길이 = 6.4f
        var m = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        Console.WriteLine($"{m} => {x / m}");

        // e.g. 정규화
        // 1. player : (-4, 1)
        // 2. enemy : (2, 3)
        var xx = 6.0f;
        var yy = 2.0f;
        var magnitude = Math.Sqrt(Math.Pow(xx, 2) + Math.Pow(yy, 2)); 
        Console.WriteLine(magnitude); // 6.324f
        // magnitude (크기) = 6.324f
        // direction (방향) = 6 / 6.324f : 2 / 6.324f
        var direction1 = xx / magnitude;
        var direction2 = yy / magnitude;
        Console.WriteLine($"{direction1:n2} : {direction2:n2}"); // 0.95f : 0.32f

    }

    // private method
    private void InitVariables()
    {
        end = false;
    }
}
