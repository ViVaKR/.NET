

using Camp.Demo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IWriter, Writer>();

using IHost host = builder.Build();

await host.RunAsync();

