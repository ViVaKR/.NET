
using Camp.Pivot;

// (1) 기초 데이터 만들기
Create(30);

// (2) 데이터 만들기 
static void Create(int count)
{
    List<Repo> list = [];
    Random random = new(); // 가상 데이터 작성용

    // 가상 선수 이름
    string[] students = ["장길산", "임꺽정", "전혁진", "이율곡", "성삼문"];

    for (int i = 0; i < count; i++)
    {
        var repo = new Repo
        {
            Id = i + 1,
            Name = students[i % students.Length],
            Runtime = DateTime.Now.AddDays(-random.Next(0, 5)),
            TypeOfItem = (Item)random.Next(0, Enum.GetValues(typeof(Item)).Length),
            NumberOfExec = random.Next(0, 10)
        };
        list.Add(repo);
    }

    Print(list, students);
    Pivot(list);
}
Console.WriteLine();
Console.WriteLine();


static void Print(IList<Repo> list, string[] students)
{
    Console.WriteLine("## DataList Print ##");
    Console.WriteLine($"이름\t실행일자\t\t종목\t횟수");
    for (int i = 0; i < students.Length; i++)
    {
        foreach (var item in list.Where(x => x.Name!.Equals(students[i])))
        {
            Console.Write($"{item.Name}\t{item.Runtime:yyyy-MM-dd}\t{item.TypeOfItem}\t{item.NumberOfExec}");
            Console.WriteLine();
        }
    }
}

Console.WriteLine();
Console.WriteLine();

// 문의에 대한 답변 출력 파트 (전체)
static void Pivot(IList<Repo> list)
{
    var pv = list.GroupBy(g => new
    {
        _이름 = g.Name,
        _시간 = g.Runtime
    }).Select(x => new
    {
        이름 = x.Key._이름,
        시간 = x.Key._시간,
        아령 = x.Where(w => w.TypeOfItem == Item.아령 && w.Name!.Equals(x.Key._이름)).Sum(g => g.NumberOfExec),
        줄넘기 = x.Where(w => w.TypeOfItem == Item.줄넘기 && w.Name!.Equals(x.Key._이름)).Sum(g => g.NumberOfExec),
        고무줄 = x.Where(w => w.TypeOfItem == Item.고무줄 && w.Name!.Equals(x.Key._이름)).Sum(g => g.NumberOfExec),
        달리기 = x.Where(w => w.TypeOfItem == Item.달리기 && w.Name!.Equals(x.Key._이름)).Sum(g => g.NumberOfExec),
    }).OrderBy(x => x.시간).ThenBy(x => x.이름);

    var fieldName = $"시간\t\t이름\t아령\t줄넘기\t고무줄\t달리기";
    var line = string.Join(string.Empty, Enumerable.Repeat("-", fieldName.Length + (4 * 8)));
    Console.WriteLine("## PIVOT Print ##");
    Console.WriteLine(line);
    Console.WriteLine(fieldName);
    Console.WriteLine(line);

    foreach (var item in pv)
    {
        Console.Write($"{item.시간:yyyy-MM-dd}\t{item.이름}\t{item.아령}\t{item.줄넘기}\t{item.고무줄}\t{item.달리기}");
        Console.WriteLine();
    }
    Console.WriteLine(line);

    PivotSimple(list);
}

// 문의에 대한 답변 출력 파트 (요약 출력)
static void PivotSimple(IList<Repo> list)
{
    var pv = list.GroupBy(g => new
    {
        _이름 = g.Name
    }).Select(x => new
    {
        이름 = x.Key._이름,
        아령 = x.Where(w => w.TypeOfItem == Item.아령 && w.Name!.Equals(x.Key._이름)).Sum(g => g.NumberOfExec),
        줄넘기 = x.Where(w => w.TypeOfItem == Item.줄넘기 && w.Name!.Equals(x.Key._이름)).Sum(g => g.NumberOfExec),
        고무줄 = x.Where(w => w.TypeOfItem == Item.고무줄 && w.Name!.Equals(x.Key._이름)).Sum(g => g.NumberOfExec),
        달리기 = x.Where(w => w.TypeOfItem == Item.달리기 && w.Name!.Equals(x.Key._이름)).Sum(g => g.NumberOfExec),
    }).OrderBy(x => x.이름);

    var fieldName = $"이름\t아령\t줄넘기\t고무줄\t달리기";
    var line = string.Join(string.Empty, Enumerable.Repeat("-", fieldName.Length + (4 * 5)));
    Console.WriteLine("## PIVOT Simple Print ##");
    Console.WriteLine(line);
    Console.WriteLine(fieldName);
    Console.WriteLine(line);

    foreach (var item in pv)
    {
        Console.Write($"{item.이름}\t{item.아령}\t{item.줄넘기}\t{item.고무줄}\t{item.달리기}");
        Console.WriteLine();
    }
    Console.WriteLine(line);
}
