
using System.CommandLine;
namespace Camp.Start;

public static partial class Program
{
    static int Main(string[] args)
    {
        var file = new Option<FileInfo?>(
            name: "--file",
            description: "The file to read and display on the console.",
            isDefault: true,
            parseArgument: result =>
            {
                if (result.Tokens.Count == 0)
                {
                    return new FileInfo("sampleQuotes.txt");
                }
                string? filePath = result.Tokens.Single().Value;
                if(!File.Exists(filePath)){
                    result.ErrorMessage = "File does not exist";
                    return null;
                }else{
                    return new FileInfo(filePath);
                }
            }

            );

        var delay = new Option<int>(
            name: "--delay",
            description: "Delay between lines",
            getDefaultValue: () => 42
        );
        var fgcolor = new Option<ConsoleColor>(
            name: "--fgcolor",
            description: "Foreground Color of Text",
            getDefaultValue: () => ConsoleColor.White
        );

        var lightMode = new Option<bool>(
            name: "--light-mode",
            description: "Background color of text displayed on the console"
        );

        var rootCommand = new RootCommand("Camp.Start App for System.CommandLine");

        var readCommnad = new Command("read", "Read and display the file."){
            file,
            delay,
            fgcolor,
            lightMode
        };

        rootCommand.AddCommand(readCommnad);

        readCommnad.SetHandler(async (file, delay, fgcolor, lightMode) =>
        {
            await ReadFile(file!, delay, fgcolor, lightMode);
        }, file, delay, fgcolor, lightMode);


        return rootCommand.InvokeAsync(args).Result;
    }

    internal static async Task ReadFile(FileInfo file, int delay, ConsoleColor fgColor, bool lightMode)
    {
        Console.BackgroundColor = lightMode ? ConsoleColor.White : ConsoleColor.Black;
        Console.ForegroundColor = fgColor;
        List<string> lines = File.ReadLines(file.FullName).ToList();

        foreach (var line in lines)
        {
            Console.WriteLine(line);
            await Task.Delay(delay * line.Length);
        }
    }

}

/* 
    ? Run
    !$ dotnet build
    !$ dotnet publish -o publish
    !$ cd publish
    !$ ./Camp.Start read --file sampleQuotes.txt --delay 1 --fgcolor Red --light-mode
 */

/*
? Console Color Options
    Black
    Blue
    Cyan
    DarkBlue
    DarkCyan
    DarkGray
    DarkGreen
    DarkMagenta
    DarkRed
    DarkYellow
    Gray
    Green
    Magenta
    Red
    White
    Yellow
 */

//#  System.CommandLine #//
//! install nuget package : https://www.nuget.org/packages/System.CommandLine
//! dotnet add package System.CommandLine --version 2.0.0-beta4.22272.1

//--> /Users/vivakr/GitWorkspace/DOTNET/WinFormNConsoleCamp/Solution.Consoles/BootCamp/Camp.Start/Camp.Start.csproj
