
using System.CommandLine;

var rootCmd = new RootCommand("Camp Args App");

var menu = new Option<int>(
    name: "--menu",
    description: "An option whose argument is parsed as an int.",
    getDefaultValue: () => 1
);

menu.AddAlias("-m");

var message = new Option<string>("--message", "An option whose argument is parsed as a string.");
message.AddAlias("-s");

var uri = new Option<Uri>("--uri") { IsRequired = true };

rootCmd.Add(menu);
rootCmd.Add(message);
rootCmd.Add(uri);


rootCmd.SetHandler((menu, message, uri) =>
{
    Console.WriteLine($"{menu} - {message} - {uri}");
}, menu, message, uri);

await rootCmd.InvokeAsync(args);
