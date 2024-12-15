namespace VivFluentApp.Components.Pages;



public partial class Code
{

    private readonly string? _title;
    public readonly List<string> _items =
    [
        "First",
        "Second",
        "Third",
        "Fourth"
    ];

    private record Person(int PersonId, string Name, DateOnly BirthDate);
    private IQueryable<Person> people;

    private bool Expanded { get; set; }

    public Code()
    {
        _title = "Chat";
        Expanded = false;
        people = new List<Person>
        {
            new(1, "John", new DateOnly(1980, 1, 1)),
            new(2, "Jane", new DateOnly(1985, 2, 2)),
            new(3, "Jack", new DateOnly(1990, 3, 3)),
            new(4, "Jill", new DateOnly(1995, 4, 4))
        }.AsQueryable();

    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }
}
