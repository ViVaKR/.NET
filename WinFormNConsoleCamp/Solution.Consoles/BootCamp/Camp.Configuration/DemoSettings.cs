namespace CampConfiguration;

public sealed class DemoSettings
{
    public required int KeyOne { get; set; }

    public required bool KeyTwo { get; set; }

    public required NestedSettings KeyThree { get; set; } = null!;
}

public sealed class NestedSettings
{
    public required string Message { get; set; } = null!;
}
