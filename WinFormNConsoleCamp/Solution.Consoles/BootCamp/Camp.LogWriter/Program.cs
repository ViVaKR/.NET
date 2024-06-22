using Camp.LogWriter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();
services.AddLogging(x=>x.AddConsole());
services.AddSingleton<ExampleService>();
IServiceProvider serviceProvider = services.BuildServiceProvider();

// Get the ExampleSerevice object from the container
ExampleService service = serviceProvider.GetRequiredService<ExampleService>();

service.DoSomeWork(12, 45);

// Console.WriteLine("Hello, World");
// 
