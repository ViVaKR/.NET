using System.CommandLine;

var option = new Option<FileInfo?>(name: "--file", description: "The file to read and display on the console.");
var command = new RootCommand("Sample app for System.CommandLine");
command.AddOption(option);
command.SetHandler(x => ReadFile(x!), option);
return await command.InvokeAsync(args);

static void ReadFile(FileInfo file)
=> File.ReadLines(file.FullName).ToList().ForEach(Console.WriteLine);

//! [App Test]
// $ dotnet publish -o publish
// $ cd publish
// $ ./Camp.CmdLine --file Camp.CmdLine.runtimeconfig.json
//! or
// $ dotnet run -- --file ./publish/Camp.CmdLine.runtimeconfig.json
//! help
// ./publish/Camp.CmdLine --help
// ./publish/Camp.CmdLine --version
