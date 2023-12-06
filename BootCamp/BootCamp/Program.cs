
using System.Linq.Expressions;
using BootCamp;


bool check;
int choice;

do
{
    Console.WriteLine("");
    check = int.TryParse(Console.ReadLine(), out  choice);

} while (!check);

if (!check) return;

switch (choice)
{
    case 1: Demo_StringCompare(); break;
    case 2: Demo_IEnumerable(); break;
}
Demo_IEnumerable();

Demo_StringCompare();

static void Demo_StringCompare()
{
    var distance = new Distance(3.14, 4.5);
    Console.WriteLine($"{distance.Magnitude} {distance.Direction}");

    var bank = new BankAccount("101", "Viv");
    Console.WriteLine(bank.ToString());


    List<int> b = [1, 2, 3, 4];

    foreach (var item in b)
    {
        Console.WriteLine(item);
    }

    Func<int, int> square = x => x * x;

    Console.WriteLine(square(5));

    Expression<Func<int, int>> e = x => x * x * 5;

    Console.WriteLine(e);

    int[] numbers = { 2, 3, 4, 5 };

    var snum = numbers.Select(x => x * x);

    Console.WriteLine(string.Join(", ", snum));


    // 연산자 => 오른 쪽에 식이 있는 람다식을 람다 라고 함.

    // 식람다
    // 식 람다는 식의 결과를 반환하며 기본 형식은 다음과 같음
    // (input-parameters) => expression

    // 문람다
    // 문람다는 다음과 같이 중괄호 안에 문을 지정한다는 점을 제회하면 식 람다와 유사함.
    // (input-parameters) => { <sequence-of-statements }

    // 람다식 입력 매개 변수
    // 매개변수는 괄호로 묶음
    // 매개변수가 0개이면 빈괄호를 지정
    // Action line = () => Console.WriteLine();
    // 람다식에 입력 매개 변수가 하나만 있는 경우 괄호는 선택 사항.
    Func<double, double> cube = x => x * x * x;
    // 형식 명시적으로 지정할 수 있음

    Func<int, string, bool> isTooLong = (int x, string s) => s.Length > x;
    // 

    var rs1 = string.Compare("A", "B");
    var rs2 = string.Compare("a", "A");
    var rs3 = string.Compare("A", "a");
    var rs4 = string.Compare("A", "A");
    var rs5 = string.Compare("h", "a");
    var rs6 = string.Compare("b", "B");

    // 0 : 두문자열 모두 동일
    // -1 : 첫번째 문자열은 두번째 문자열 보다 작음
    // 1 : 첫번째 문자열은 두번째 문자열 보다 큼
    Console.WriteLine();
    Console.WriteLine("h vs a = " + rs5);
    Console.WriteLine("b vs B = " + rs6);
    Console.WriteLine();
    Console.WriteLine("A(65), B(66) = " + rs1);
    Console.WriteLine("a(97), A(65) = " + rs2);
    Console.WriteLine("A(65), a(97) = " + rs3);
    Console.WriteLine("A, A = " + rs4);
    Console.WriteLine($"A : {(int)'A'}, a : {(int)'a'}, B : {(int)'B'}");


    // 사전 정렬 순서 : 
    // 오름 차순 순서 : (a, A, B, b, c, C, D, d), 
    // ----> 텍스트 값으로 평가될 때 소문자 대문자 구분없이 "a" 는 "A" 앞에 나타나고 
    // ----> "B" 는 "b" 앞에 나타남

    // 대소문자 구분 순서 : ASCII 문자값을 기준으로 텍스트 정렬 (A, B, C, D, a, b, c, d)
    char[] c = { 'h', 'c', 'Z', 'a', 'A', 'B', 'c' };
    Console.WriteLine(string.Join(", ", c.ToList().OrderBy(x => x)));

    string[] s = { "A", "k", "a", "B" };
    Console.WriteLine(string.Join(", ", s.ToList().OrderBy(x => x)));

    // Compare(string strA, int indexA, string strB, int indexB, int length)
    int result = string.Compare("applepie", 5, "pie", 0, 3);
    Console.WriteLine(result);


    IEnumerable<double> doubles = new List<double> { 1.2, 1.7, 2.5, 2.4 };

    bool rs10 = doubles.Any(x => x < 1);
    Console.WriteLine(rs10);

    bool rs11 = doubles.All(x => x > 1);
    Console.WriteLine(rs11);
}

static void Demo_IEnumerable()
{
    Person[] peopleArray = [
            new Person("John", 45),
            new Person("Sue", 35),
            new Person("Viv", 27)
        ];

    People peopleList = new(peopleArray);
    Console.WriteLine();
    foreach (Person p in peopleList)
    {
        Console.WriteLine($"{p.FullName} - {p.Age}");
    }
}
