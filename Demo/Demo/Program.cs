using System.Linq;

int[] numbers = [0, 1, 2, 3, 4, 5, 6, 7];

var query = from num in numbers
            where (num % 2) == 0
            select num;


var str = string.Join(", ", query);

Console.WriteLine($"{str}");
